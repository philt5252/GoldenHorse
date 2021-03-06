﻿using System;
using System.Threading;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class DelayOperation : Operation
    {
        private OperationParameter delayParameter { get { return Parameters[0]; } }

        public override string Name
        {
            get { return "Delay"; }
        }

        public int Delay
        {
            get { return int.Parse(delayParameter.Value.ToString()); }
            set
            {
                delayParameter.Value = value;
            }
        }

        public override string ParametersDescription
        {
            get { return delayParameter.Value.ToString() + " seconds"; }
        }

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name = "Delay (seconds)",
                Mode = OperationParameterValueMode.Constant
            };

            return new []{param1};
        }

        public override string DefaultDescription(MappedItem control)
        {
            return string.Format("Delays the execution by {0} seconds.", delayParameter.Value);
        }

        public override bool Play(MappedItem control, Log log)
        {
            double seconds;

            if (!double.TryParse(delayParameter.GetValue(), out seconds))
            {
                log.CreateLogItem(LogItemCategory.Error, "Not a valid value for seconds", null);
                return false;
            }

            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            log.CreateLogItem(LogItemCategory.Event, string.Format("Delayed {0} second(s)", seconds));

            return true;
        }
    }
}