﻿using System;
using PondSharp.UserScripts;

namespace PondSharp.Examples
{
    public class ExampleTestEntity : AbstractTestEntity
    {
        private readonly Random _random = new Random();
        
        public ExampleTestEntity(string id, AbstractEntityController controller) : base(id, controller)
        {
        }
        
        public override void Tick()
        {
            var xChange = _random.Next(-1, 2);
            var yChange = _random.Next(-1, 2);
            MoveTo(X + xChange, Y + yChange);
        }

    }
}