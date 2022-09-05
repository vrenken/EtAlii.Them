using DeBroglie.Wfc;
using System;

namespace DeBroglie.Trackers
{
    class WeightedRandomPatternPicker : IPatternPicker
    {
        private Wave wave;

        private double[] frequencies;

        public void Init(WavePropagator wavePropagator)
        {
            wave = wavePropagator.Wave;
            frequencies = wavePropagator.Frequencies;
        }

        public int GetRandomPossiblePatternAt(int index, Func<double> randomDouble)
        {
            return RandomPickerUtils.GetRandomPossiblePattern(wave, randomDouble, index, frequencies);
        }
    }
}
