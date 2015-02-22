using System;
using NinEngine;

namespace NinCmd
{
    public class Validator
    {
        private readonly ValidationKind _validationKind;

        public Validator(ValidationKind validationKind)
        {
            _validationKind = validationKind;
        }

        public OperationResult ValidateOne(string number)
        {
            OperationResult result = Validate(number);
            return result;
        }

        public OperationResult RepeatValidation()
        {
            OperationResult result = new OperationResult {Code = Statuscode.Ok};
            string number = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(number))
            {
                result = Validate(number);
                Console.WriteLine(result.ToString());
                number = Console.ReadLine();
            }
            return result;
        }

        private OperationResult Validate(string number)
        {
            OperationResult result;
            switch (_validationKind)
            {
                case ValidationKind.OrganizationNumber:
                    result = ValidateOrganizationNumber(number);
                    break;
                case ValidationKind.BirthNumber:
                    result = ValidateBirthNumber(number);
                    break;
                case ValidationKind.DNumber:
                    result = ValidateDNumber(number);
                    break;
                default:
                    result = ValidateAnyIdNumber(number);
                    break;
            }
            return result;
        }

        private static OperationResult ValidateOrganizationNumber(string number)
        {
            try
            {
                OrganizationNumber organizationNumber = new OrganizationNumber(number);
                return new OperationResult {Code = Statuscode.Ok};
            }
            catch (NinException ex)
            {
                return new OperationResult {Code = ex.Code, Message = ex.Message};
            }
        }

        private static OperationResult ValidateBirthNumber(string number)
        {
            try
            {
                BirthNumber birthNumber = new BirthNumber(number);
                return new OperationResult { Code = Statuscode.Ok };
            }
            catch (NinException ex)
            {
                return new OperationResult { Code = ex.Code, Message = ex.Message };
            }
        }

        private static OperationResult ValidateDNumber(string number)
        {
            try
            {
                DNumber dNumber = new DNumber(number);
                return new OperationResult { Code = Statuscode.Ok };
            }
            catch (NinException ex)
            {
                return new OperationResult { Code = ex.Code, Message = ex.Message };
            }
        }

        private static OperationResult ValidateAnyIdNumber(string number)
        {
            OrganizationNumber on = OrganizationNumber.Create(number);
            if (on != null)
            {
                return new OperationResult { Code = Statuscode.Ok, Message = "Organization number"};
            }
            BirthNumber bn = BirthNumber.Create(number);
            if (bn != null)
            {
                return new OperationResult { Code = Statuscode.Ok, Message = "Birth number" };
            }
            DNumber dn = DNumber.Create(number);
            if (dn != null)
            {
                return new OperationResult { Code = Statuscode.Ok, Message = "D-number" };
            }
            return new OperationResult { Code = Statuscode.NoMatchFound, Message = "Neither organization number, birth number nor D-number."};
        }
    }
}
