﻿using System;

namespace Bau.Libraries.LibMotionComic.Model.Components.Transforms
{
	/// <summary>
	///		Transformación de rotación
	/// </summary>
	public class RotateTransformModel : AbstractTransformModel
	{
		public RotateTransformModel(double dblAngle, Entities.PointModel pntOrigin)
		{ Angle = dblAngle;
			Origin = pntOrigin;
		}

		/// <summary>
		///		Angulo de rotación
		/// </summary>
		public double Angle { get; set; }

		/// <summary>
		///		Origen de la rotación
		/// </summary>
		public Entities.PointModel Origin { get; set; }
	}
}
