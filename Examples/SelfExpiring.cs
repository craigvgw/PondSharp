using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using PondSharp.UserScripts;

namespace PondSharp.Examples
{
    /// <summary>
    /// This entity moves randomly.
    /// </summary>
    [PondDefaults(Name = "Burn Effect")]
    public class SelfExpiring : Simple
    {
        private static int StartAgeReal = 80;
        
        [PondAdjustable(Min = 5, Max=240, Name = "Age")]
        private static int StartAge
        {
            get => StartAgeReal;
            // ReSharper disable once UnusedMember.Local
            set
            {
                StartAgeReal = value;
                Colors = CreateAgeColor();
            }
        }

        [PondAdjustable(Min = 0, Max = 1, Step = 0.05)]
        private static double Ratio { get; set; } = 0.8;
        
        private int _remainingLife;
        private int _age;
        
        protected override void OnCreated()
        {
            base.OnCreated();
            SetIsBlocking(false);
            var mid = (1 - Ratio) * StartAge;
            _age = (int)(Random.NextDouble() <= Ratio ? mid - Math.Pow(mid, Random.NextDouble()) : Math.Pow(StartAge - mid, Random.NextDouble()) + mid);
            _remainingLife = StartAge - _age;
        }

        protected override void Tick()
        {
            // Because the change of the age will
            // change the length of the Colors
            // array, we need to ensure we do not
            // try to change the color past colors
            // that are available.
            if (Colors.Count <= _age)
            {
                Destroy();
                return;
            }
            
            ChangeColor(Colors[_age]);
            _age++;
            if (--_remainingLife <= 0)
            {
                Destroy();
                return;
            }

            // if (Random.Next(10) == 0)
                // ResetPower();
            
            base.Tick();

        }
        
        private static readonly (double, Color)[] ColorsTransitions =
        {
            (0, System.Drawing.Color.FromArgb(100, 0 , 1)),
            (0.3, System.Drawing.Color.Firebrick),
            (0.6, System.Drawing.Color.Yellow),
            (0.9, System.Drawing.Color.FromArgb(50, 0, 1)),
            (1, System.Drawing.Color.Black)
        };
            
        private static List<int> Colors = CreateAgeColor();
        
        private static List<int> CreateAgeColor()
        {
            Debug.Assert(ColorsTransitions.Length > 1);
            var colors = Enumerable.Empty<Color>();

            for (var i = 0; i < ColorsTransitions.Length - 1; i++)
            {
                var (startPercentage, startColor) = ColorsTransitions[i];
                var (nextPercentage, nextColor) = ColorsTransitions[i + 1];
                var total = nextPercentage - startPercentage;
                var count = (int)(total * StartAge);
                colors = colors.Concat(GenerateSequence(startColor, nextColor, i == 0 ? count : count + 1).Skip(i == 0 ? 0 : 1));
            }
            return colors.Select(c => c.ToArgb()).ToList();
        }
        
        private static IEnumerable<Color> GenerateSequence(Color start, Color end, int colorCount)
        {
            for (var n = 0; n < colorCount; n++)
            {
                var ratio = n / (double)(colorCount - 1);
                var negativeRatio = 1.0 - ratio;
                var aComponent = negativeRatio * start.A + ratio * end.A;
                var rComponent = negativeRatio * start.R + ratio * end.R;
                var gComponent = negativeRatio * start.G + ratio * end.G;
                var bComponent = negativeRatio * start.B + ratio * end.B;

                yield return System.Drawing.Color.FromArgb((byte)aComponent, (byte)rComponent, (byte)gComponent, (byte)bComponent);
            }
        }
    }
    
    
}