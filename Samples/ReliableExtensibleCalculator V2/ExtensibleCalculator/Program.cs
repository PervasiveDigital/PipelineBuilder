using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.AddIn.Hosting;
using Calculators.Extensibility;

namespace ExtensibleCalculator
{
    class Program
    {
        static void Main()
        {
            //In this sample we expect the AddIns and components to be installed in the current directory
            AddInToken calcToken = GetToken();

           
            //Activate the selected AddInToken in a new AppDomain

            Calculator calculator = ActivateCalculator(calcToken); 
            while (RunCalculator(calculator))
            {
                calcToken = GetToken();
                calculator = ActivateCalculator(calcToken);

            } ;
          
            //Run the read-eval-print loop
            
        }

        private static Calculator ActivateCalculator(AddInToken calcToken)
        {
            Calculator calculator;
            calculator = calcToken.Activate<Calculator>(AddInSecurityLevel.FullTrust);
            return calculator;
        }

        private static AddInToken GetToken()
        {
            String addInRoot = Environment.CurrentDirectory;

            //Check to see if new AddIns have been installed
            AddInStore.Rebuild(addInRoot);

            //Look for Calculator AddIns in our root directory and store the results
            Collection<AddInToken> tokens = AddInStore.FindAddIns(typeof(Calculator), addInRoot);

            //Ask the user which AddIn they would like to use
            AddInToken calcToken = ChooseCalculator(tokens);
            return calcToken;
        }

        private static AddInToken ChooseCalculator(Collection<AddInToken> tokens)
        {
            List<AddInToken> unreliableTokens = UnhandledExceptionHelper.GetUnreliableTokens();
            if (tokens.Count == 0)
            {
                Console.WriteLine("No calculators are available");
                return null;
            }
            Console.WriteLine("Available Calculators: ");
            for (int i = 0; i < tokens.Count; i++)
            {
                String warning = "";
                foreach (AddInToken token in unreliableTokens){
                    if (tokens[i].AssemblyName.FullName.Equals(token.AssemblyName.FullName))
                    {
                        warning = "{ Possibly Unreliable }";
                        break;
                    }
                }
                
                Console.WriteLine(String.Format("\t{0}. {1}{2}", (i + 1).ToString(), tokens[i].Name, warning));
                
            }
            Console.WriteLine("Which calculator do you want to use?");
            String line = Console.ReadLine();
            int selection;
            if (Int32.TryParse(line, out selection))
            {
                if (selection <= tokens.Count)
                {
                    return tokens[selection - 1];
                }
            }
            Console.WriteLine("Invalid selection: {0}. Please choose again.", line);
            return ChooseCalculator(tokens);
        }

        private static bool RunCalculator(Calculator calc)
        {
            
            if (calc == null)
            {
                //No calculators were found, read a line and exit
                Console.ReadLine();
            }
            UnhandledExceptionHelper.LogUnhandledExceptions(calc);
            Console.WriteLine("Available operations: " + calc.Operations);
            Console.WriteLine("Type \"exit\" to exit, type \"reload\" to reload");
            String line = Console.ReadLine();
            while (!line.Equals("exit"))
            {
                if (line.Equals("reload"))
                {
                    return true;
                }
                //We have a very simple parser, if anything unexpected happens just ask the user to try again.  
                try
                {
                    Command c = new Command(line);
                    Console.WriteLine(calc.Operate(new MyOperate(c.Action, c.A, c.B)));
                }
                catch
                {
                    Console.WriteLine("Invalid command: {0}. Commands must be formated: [number] [operation] [number]", line);
                    Console.WriteLine("Available operations: " + calc.Operations);
                }

                line = Console.ReadLine();
            }
         
            return false;
        }
    }

    

    internal class MyOperate : Operate
    {
        String _operation;
        double _a;
        double _b;

        public MyOperate(String operation, double a, double b)
        {
            _operation = operation;
            _a = a;
            _b = b;
        }

        public override string Operation
        {
            get
            {
                return _operation;
            }
        }

        public override double A
        {
            get { return _a; }
        }

        public override double B
        {
            get { return _b; }
        }
    }


    internal class Command
    {
        internal Command(String line)
        {
            String[] parts = line.Trim().Split(' ');
            a = Double.Parse(parts[0]);
            action = parts[1];
            b = Double.Parse(parts[2]);
        }

        double a;

        public double A
        {
            get { return a; }
        }
        double b;

        public double B
        {
            get { return b; }
        }
        String action;

        public String Action
        {
            get { return action; }
        }
    }
}
