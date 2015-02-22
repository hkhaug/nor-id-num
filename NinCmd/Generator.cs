using System;
using System.Collections.Generic;
using System.Linq;
using NinEngine;

namespace NinCmd
{
    public class Generator
    {
        private readonly GenerationKind _generationKind;

        public Generator(GenerationKind generationKind)
        {
            _generationKind = generationKind;
        }

        public OperationResult Generate(int count)
        {
            OperationResult result;
            try
            {
                switch (_generationKind)
                {
                    case GenerationKind.OrganizationNumber:
                        if (count > 1)
                        {
                            IEnumerable<OrganizationNumber> ons = OrganizationNumber.ManyRandom(count);
                            result = WriteList(ons.Select(x => x.Number));
                        }
                        else
                        {
                            OrganizationNumber on = OrganizationNumber.OneRandom();
                            result = WriteNumber(on);
                        }
                        break;
                    case GenerationKind.BirthNumber:
                        if (count > 1)
                        {
                            IEnumerable<BirthNumber> bns = BirthNumber.ManyRandom(count);
                            result = WriteList(bns.Select(x => x.Number));
                        }
                        else
                        {
                            BirthNumber bn = BirthNumber.OneRandom();
                            result = WriteNumber(bn);
                        }
                        break;
                    case GenerationKind.DNumber:
                        if (count > 1)
                        {
                            IEnumerable<DNumber> dns = DNumber.ManyRandom(count);
                            result = WriteList(dns.Select(x => x.Number));
                        }
                        else
                        {
                            DNumber dn = DNumber.OneRandom();
                            result = WriteNumber(dn);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (NinException ex)
            {
                result = new OperationResult { Code = ex.Code, Message = ex.Message };
            }
            return result;
        }

        public OperationResult Generate(string pattern)
        {
            OperationResult result;
            try
            {
                switch (_generationKind)
                {
                    case GenerationKind.OrganizationNumber:
                        OrganizationNumber on = OrganizationNumber.OneRandom(pattern);
                        result = WriteNumber(on);
                        break;
                    case GenerationKind.BirthNumber:
                        BirthNumber bn = BirthNumber.OneRandom(pattern);
                        result = WriteNumber(bn);
                        break;
                    case GenerationKind.DNumber:
                        DNumber dn = DNumber.OneRandom(pattern);
                        result = WriteNumber(dn);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (NinException ex)
            {
                result = new OperationResult { Code = ex.Code, Message = ex.Message };
            }
            return result;
        }

        public OperationResult Generate(DateTime dateFrom, DateTime dateTo, GenderRequest gender)
        {
            OperationResult result;
            try
            {
                switch (_generationKind)
                {
                    case GenerationKind.BirthNumber:
                        BirthNumber bn = BirthNumber.OneRandom(dateFrom, dateTo, gender);
                        result = WriteNumber(bn);
                        break;
                    case GenerationKind.DNumber:
                        DNumber dn = DNumber.OneRandom(dateFrom, dateTo, gender);
                        result = WriteNumber(dn);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (NinException ex)
            {
                result = new OperationResult { Code = ex.Code, Message = ex.Message };
            }
            return result;
        }

        private static OperationResult WriteList(IEnumerable<string> items)
        {
            bool any = false;
            foreach (string item in items)
            {
                any = true;
                WriteItem(item);
            }
            return any
                ? new OperationResult {Code = Statuscode.Ok, Message = "Ok"}
                : new OperationResult {Code = Statuscode.NoMatchFound, Message = "No ID number found."};
        }

        private static OperationResult WriteNumber(IdNumberBase number)
        {
            if (null == number)
            {
                return new OperationResult { Code = Statuscode.NoMatchFound, Message = "No ID number found." };
            }
            WriteItem(number.Number);
            return new OperationResult {Code = Statuscode.Ok, Message = "Ok"};
        }

        private static void WriteItem(string item)
        {
            Console.WriteLine(item);
        }
    }
}
