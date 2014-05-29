﻿using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class ClickOperation : Operation
    {
        public ClickOperation()
        {
            OperationParameter param1 = new OperationParameter
            {
                Name = "ClientX",
                Mode = OperationParameterValueMode.Constant
            };

            OperationParameter param2 = new OperationParameter
            {
                Name = "ClientY",
                Mode = OperationParameterValueMode.Constant
            };

            Parameters.Add(param1);
            Parameters.Add(param2);
        }
    }
}