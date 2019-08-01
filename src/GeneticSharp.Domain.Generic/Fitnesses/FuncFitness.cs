using System;
using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Infrastructure.Framework.Commons;

namespace GeneticSharp.Domain.Fitnesses.Generic
{
	/// <summary>
	/// An IFitness implementation that defer the fitness evaluation to a Func.
	/// </summary>
	public class FuncFitness<T> : IFitness<T>
	{
		private readonly Func<IChromosome<T>, double> m_func;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:GeneticSharp.Domain.Fitnesses.Generic.FuncFitness"/> class.
		/// </summary>
		/// <param name="func">The fitness evaluation Func.</param>
		public FuncFitness (Func<IChromosome<T>, double> func)
		{
			ExceptionHelper.ThrowIfNull("func", func);
			m_func = func;
		}

		#region IFitness implementation
		/// <summary>
		/// Evaluate the specified chromosome.
		/// </summary>
		/// <param name="chromosome">Chromosome.</param>
		public double Evaluate (IChromosome<T> chromosome)
		{
			return m_func (chromosome);
		}
		#endregion
	}
}