using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TspWpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Initialize();
            if (IsInDesignMode)
            {
                GA.DesignStart();
                return;
            }
            RunCommand = new RelayCommand(async () =>
                await GA.StartAsync().ConfigureAwait(false));
            CancelCommand = new RelayCommand(GA.Stop);
        }

        public RelayCommand RunCommand { get; }
        public RelayCommand CancelCommand { get; }

        public void Initialize()
        {
            GA = new TspGAGeneric();
            GA.Initialize(NumCities, SizeX, SizeY);

            RaisePropertyChanged(nameof(Cities));
            GA.GenerationRan += GA_GenerationRan;
        }

        private void GA_GenerationRan()
        {
            RaisePropertyChanged(nameof(BestLoop));
            RaisePropertyChanged(nameof(BestDistance));
        }

        public int NumCities { get; set; } = 40;
        public int SizeX { get; set; } = 300;
        public int SizeY { get; set; } = 300;

        public TspGAGeneric GA { get; set; }

        public IList<TspCity> Cities => GA?.Cities;

        public IList<TspCity> BestLoop {
            get
            {
                var genes = GA?.BestChromosome?.GetGenes();
                if (genes == null || genes.Count == 0) return new TspCity[0];
                return genes.Concat(new[] { genes.First() }).ToList();
            }
        }

        public double BestDistance => 
            (GA?.BestChromosome as TspChromosomeGeneric)?.Distance 
                ?? 0.0;
    }
}