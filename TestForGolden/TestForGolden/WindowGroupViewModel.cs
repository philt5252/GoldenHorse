﻿using System.Collections.Generic;

namespace TestForGolden
{
    public class WindowGroupViewModel : TestItemViewModelBase
    {
        public string WindowId { get; set; }
        public override string Name { get; set; }
        public override string Operation { get; set; }
        public override string Value { get; set; }
        public override string Description { get; set; }
        public override string AutowaitTimeout { get; set; }
    }
}