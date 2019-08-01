using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Infrastructure.Framework.Commons;

namespace GeneticSharp.Domain.Populations.Generic
{
    /// <summary>
    /// Represents a population of candidate solutions (chromosomes).
    /// </summary>
    public class Population<T> : IPolulation<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Domain.Populations.Generic.Population<T>"/> class.
        /// </summary>
        /// <param name="minSize">The minimum size (chromosomes).</param>
        /// <param name="maxSize">The maximum size (chromosomes).</param>
        /// <param name="adamChromosome">The original chromosome of all population ;).</param>
        public Population(int minSize, int maxSize, IChromosome<T> adamChromosome)
        {
            if (minSize < 2)
            {
                throw new ArgumentOutOfRangeException("minSize", "The minimum size for a population is 2 chromosomes.");
            }

            if (maxSize < minSize)
            {
                throw new ArgumentOutOfRangeException("maxSize", "The maximum size for a population should be equal or greater than minimum size.");
            }

            ExceptionHelper.ThrowIfNull("adamChromosome", adamChromosome);

            CreationDate = DateTime.Now;
            MinSize = minSize;
            MaxSize = maxSize;
            AdamChromosome = adamChromosome;
            Generations = new List<Generation<T>>();
            GenerationStrategy = new PerformanceGenerationStrategy<T>(10);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when best chromosome changed.
        /// </summary>
        public event EventHandler BestChromosomeChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreationDate { get; protected set; }

        /// <summary>
        /// Gets or sets the generations.
        /// <remarks>
        /// The information of Generations can vary depending of the IGenerationStrategy used.
        /// </remarks>
        /// </summary>
        /// <value>The generations.</value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Parent classes need to set it.")]
        public IList<Generation<T>> Generations { get; protected set; }

        /// <summary>
        /// Gets or sets the current generation.
        /// </summary>
        /// <value>The current generation.</value>
        public Generation<T> CurrentGeneration { get; protected set; }

        /// <summary>
        /// Gets or sets the total number of generations executed.
        /// <remarks>
        /// Use this information to know how many generations have been executed, because Generations.Count can vary depending of the IGenerationStrategy used.
        /// </remarks>
        /// </summary>
        public int GenerationsNumber { get; protected set; }

        /// <summary>
        /// Gets or sets the minimum size.
        /// </summary>
        /// <value>The minimum size.</value>
        public int MinSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the max.
        /// </summary>
        /// <value>The size of the max.</value>
        public int MaxSize { get; set; }

        /// <summary>
        /// Gets or sets the best chromosome.
        /// </summary>
        /// <value>The best chromosome.</value>
        public IChromosome<T> BestChromosome { get; protected set; }

        /// <summary>
        /// Gets or sets the generation strategy.
        /// </summary>
        public IGenerationStrategy<T> GenerationStrategy { get; set; }

        /// <summary>
        /// Gets or sets the original chromosome of all population.
        /// </summary>
        /// <value>The adam chromosome.</value>
        protected IChromosome<T> AdamChromosome { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates the initial generation.
        /// </summary>
        public virtual void CreateInitialGeneration()
        {
            Generations = new List<Generation<T>>();
            GenerationsNumber = 0;

            var chromosomes = new List<IChromosome<T>>();

            for (int i = 0; i < MinSize; i++)
            {
                var c = AdamChromosome.CreateNew();

                if (c == null)
                {
                    throw new InvalidOperationException("The Adam chromosome's 'CreateNew' method generated a null chromosome. This is a invalid behavior, please, check your chromosome code.");
                }

                c.ValidateGenes();

                chromosomes.Add(c);
            }

            CreateNewGeneration(chromosomes);
        }

        /// <summary>
        /// Creates a new generation.
        /// </summary>
        /// <param name="chromosomes">The chromosomes for new generation.</param>
        public virtual void CreateNewGeneration(IList<IChromosome<T>> chromosomes)
        {
            ExceptionHelper.ThrowIfNull("chromosomes", chromosomes);
            chromosomes.ValidateGenes();

            CurrentGeneration = new Generation<T>(++GenerationsNumber, chromosomes);
            Generations.Add(CurrentGeneration);
            GenerationStrategy.RegisterNewGeneration(this);
        }

        /// <summary>
        /// Ends the current generation.
        /// </summary>        
        public virtual void EndCurrentGeneration()
        {
            CurrentGeneration.End(MaxSize);

            if (BestChromosome != CurrentGeneration.BestChromosome)
            {
                BestChromosome = CurrentGeneration.BestChromosome;

                OnBestChromosomeChanged(EventArgs.Empty);
            }
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Raises the best chromosome changed event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        protected virtual void OnBestChromosomeChanged(EventArgs args)
        {
            BestChromosomeChanged?.Invoke(this, args);
        }
        #endregion
    }
}