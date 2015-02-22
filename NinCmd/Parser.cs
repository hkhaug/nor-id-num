using System;
using System.Collections.Generic;
using NinEngine;

namespace NinCmd
{
    public enum ValidationKind { Any, OrganizationNumber, BirthNumber, DNumber };
    public enum GenerationKind { Unknown, OrganizationNumber, BirthNumber, DNumber };

    public class Parser
    {
        private enum Operation { Unknown, Generation, Validation };

        private readonly Queue<string> _parameters;
        private bool _quiet;

        public Parser(Queue<string> parameters)
        {
            _parameters = parameters;
        }

        public int Parse()
        {
            CheckQuietness();
            if (!_quiet)
            {
                WriteHeading();
            }
            OperationResult result;
            Operation operation = CheckPrimaryOperation();
            switch (operation)
            {
                case Operation.Generation:
                    result = CheckGeneration();
                    ShowResult(result);
                    return (int) result.Code;
                case Operation.Validation:
                    result = CheckValidation();
                    ShowResult(result);
                    return (int)result.Code;
                default:
                    WriteSyntax();
                    return -1;
            }
        }

        private void WriteHeading()
        {
            Console.WriteLine("Norwegian Identity Numbers including NinCmd Copyright © 2015 Hans Kristian Haug");
        }

        private void WriteSyntax()
        {
            if (_quiet)
            {
                WriteHeading();
            }
            Console.WriteLine("Syntax:");
            Console.WriteLine("NinCmd [Quiet] Validate [{Org-number|Birth-number|D-number}] {Repeat|number}");
            Console.WriteLine("  Validate either specified kind of ID number, or any kind if not specified.");
            Console.WriteLine("  Last parameter is either an ID number to be validated or Repeat.");
            Console.WriteLine("  Repeat: Read and validate ID numbers from standard input until empty line.");
            Console.WriteLine("  Return value is either 0 for valid or a positive number for invalid.");
            Console.WriteLine("  If non-quiet or repeat, result is written to standard output.");
            Console.WriteLine("NinCmd [Quiet] Generate {Org-number|Birth-number|D-number} [{pattern|count}]");
            Console.WriteLine("  Randomly generate specified type of ID number. Generated number(s) are");
            Console.WriteLine("  written to standard output.");
            Console.WriteLine("  Optionally use a pattern consisting of question marks or digits to specify");
            Console.WriteLine("  randomness of each position, or a count of numbers to be generated.");
            Console.WriteLine("  Warning: Slow operation when count is specified.");
            Console.WriteLine("NinCmd [Quiet] Generate {Birth-number|D-number} From To [{Female|Male}]");
            Console.WriteLine("  Randomly generate specified type of ID number.");
            Console.WriteLine("  Both From and To must be specified. Format is dd.mm.yyyy. Legal range is");
            Console.WriteLine("  01.01.1854 through 31.12.2039.");
            Console.WriteLine("Only first letter of each parameter is interpreted. Also parameter order is");
            Console.WriteLine("significant, but letter case is not.");
        }

        private void ShowResult(OperationResult result)
        {
            if ((int) result.Code < 0)
            {
                WriteSyntax();
            }
            else if (!_quiet)
            {
                Console.WriteLine(result.ToString());
            }
        }

        private void CheckQuietness()
        {
            if (_parameters.Count > 0)
            {
                string param = _parameters.Peek();
                if (param.StartsWith("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    _quiet = true;
                    _parameters.Dequeue();
                }
            }
        }

        private Operation CheckPrimaryOperation()
        {
            if (_parameters.Count > 0)
            {
                string param = _parameters.Dequeue();
                if (param.StartsWith("G", StringComparison.InvariantCultureIgnoreCase))
                {
                    return Operation.Generation;
                }
                if (param.StartsWith("V", StringComparison.InvariantCultureIgnoreCase))
                {
                    return Operation.Validation;
                }
            }
            return Operation.Unknown;
        }

        private OperationResult CheckValidation()
        {
            OperationResult result;
            ValidationKind validationKind = CheckValidationKind();
            bool repeat = CheckValidationRepeat();
            Validator validator = new Validator(validationKind);
            if (repeat)
            {
                result = validator.RepeatValidation();
            }
            else
            {
                if (_parameters.Count > 0)
                {
                    string number = _parameters.Dequeue();
                    result = validator.ValidateOne(number);
                }
                else
                {
                    result = new OperationResult {Code = (Statuscode) (-1)};
                }
            }
            return result;
        }

        private ValidationKind CheckValidationKind()
        {
            if (_parameters.Count > 0)
            {
                string param = _parameters.Peek();
                if (param.StartsWith("O", StringComparison.InvariantCultureIgnoreCase))
                {
                    _parameters.Dequeue();
                    return ValidationKind.OrganizationNumber;
                }
                if (param.StartsWith("B", StringComparison.InvariantCultureIgnoreCase))
                {
                    _parameters.Dequeue();
                    return ValidationKind.BirthNumber;
                }
                if (param.StartsWith("D", StringComparison.InvariantCultureIgnoreCase))
                {
                    _parameters.Dequeue();
                    return ValidationKind.DNumber;
                }
            }
            return ValidationKind.Any;
        }

        private bool CheckValidationRepeat()
        {
            if (_parameters.Count > 0)
            {
                string param = _parameters.Peek();
                if (param.StartsWith("R", StringComparison.InvariantCultureIgnoreCase))
                {
                    _parameters.Dequeue();
                    return true;
                }
            }
            return false;
        }

        private OperationResult CheckGeneration()
        {
            OperationResult result;
            GenerationKind generationKind = CheckGenerationKind();
            if (generationKind == GenerationKind.Unknown)
            {
                result = new OperationResult { Code = (Statuscode)(-1) };
            }
            else
            {
                Generator generator = new Generator(generationKind);
                DateTime dateFrom = DateTime.MinValue;
                if ((generationKind != GenerationKind.OrganizationNumber) && NextParamIsDate(ref dateFrom))
                {
                    DateTime dateTo = DateTime.MaxValue;
                    if (NextParamIsDate(ref dateTo))
                    {
                        GenderRequest gender = CheckGender();
                        result = generator.Generate(dateFrom, dateTo, gender);
                    }
                    else
                    {
                        result = new OperationResult { Code = (Statuscode)(-1) };
                    }
                }
                else
                {
                    string pattern = CheckPattern();
                    if (string.IsNullOrWhiteSpace(pattern))
                    {
                        int count = CheckCount();
                        result = generator.Generate(count);
                    }
                    else
                    {
                        result = generator.Generate(pattern);
                    }
                }
            }
            return result;
        }

        private GenerationKind CheckGenerationKind()
        {
            if (_parameters.Count > 0)
            {
                string param = _parameters.Peek();
                if (param.StartsWith("O", StringComparison.InvariantCultureIgnoreCase))
                {
                    _parameters.Dequeue();
                    return GenerationKind.OrganizationNumber;
                }
                if (param.StartsWith("B", StringComparison.InvariantCultureIgnoreCase))
                {
                    _parameters.Dequeue();
                    return GenerationKind.BirthNumber;
                }
                if (param.StartsWith("D", StringComparison.InvariantCultureIgnoreCase))
                {
                    _parameters.Dequeue();
                    return GenerationKind.DNumber;
                }
            }
            return GenerationKind.Unknown;
        }

        private bool NextParamIsDate(ref DateTime date)
        {
            if (_parameters.Count > 0)
            {
                string dateStr = _parameters.Peek();
                if (IsDate(dateStr, ref date))
                {
                    _parameters.Dequeue();
                    return true;
                }
            }
            return false;
        }

        private static bool IsDate(string dateStr, ref DateTime date)
        {
            if (dateStr.Length != 10) return false;
            if (dateStr[2] != '.') return false;
            if (dateStr[5] != '.') return false;
            int dd;
            if (!int.TryParse(dateStr.Substring(0, 2), out dd)) return false;
            int mm;
            if (!int.TryParse(dateStr.Substring(3, 2), out mm)) return false;
            int yyyy;
            if (!int.TryParse(dateStr.Substring(6, 4), out yyyy)) return false;
            if ((yyyy < 1854) || (yyyy > 2039)) return false;
            try
            {
                DateTime dt = new DateTime(yyyy, mm, dd);
                date = dt;
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        private GenderRequest CheckGender()
        {
            if (_parameters.Count > 0)
            {
                string param = _parameters.Peek();
                if (param.StartsWith("F", StringComparison.InvariantCultureIgnoreCase))
                {
                    _parameters.Dequeue();
                    return GenderRequest.Female;
                }
                if (param.StartsWith("M", StringComparison.InvariantCultureIgnoreCase))
                {
                    _parameters.Dequeue();
                    return GenderRequest.Male;
                }
            }
            return GenderRequest.Any;
        }

        private int CheckCount()
        {
            if (_parameters.Count > 0)
            {
                string param = _parameters.Peek();
                int count;
                if (int.TryParse(param, out count) && (count > 0))
                {
                    return count;
                }
            }
            return 0;
        }

        private string CheckPattern()
        {
            if (_parameters.Count > 0)
            {
                string param = _parameters.Peek();
                if (IsPattern(param))
                {
                    _parameters.Dequeue();
                    return param;
                }
            }
            return null;
        }

        private static bool IsPattern(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) return false;
            bool wildcardFound = false;
            foreach (char ch in pattern)
            {
                if (ch == '?')
                {
                    wildcardFound = true;
                }
                else if (!char.IsDigit(ch)) return false;
            }
            return wildcardFound;
        }
    }
}
