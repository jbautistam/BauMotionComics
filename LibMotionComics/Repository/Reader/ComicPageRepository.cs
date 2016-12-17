using System;
using System.Collections.Generic;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMotionComic.Model;
using Bau.Libraries.LibMotionComic.Model.Components;
using Bau.Libraries.LibMotionComic.Model.Components.PageItems;

namespace Bau.Libraries.LibMotionComic.Repository.Reader
{
	/// <summary>
	///		Repository para la carga de datos de páginas
	/// </summary>
	internal class ComicPageRepository
	{
		internal ComicPageRepository(ComicReaderMediator objMediator)
		{ Mediator = objMediator;
		}

		/// <summary>
		///		Carga los datos de una página
		/// </summary>
		internal void LoadPage(MLNode objMLNode, ComicModel objComic)
		{ PageModel objPage = new PageModel(objComic);

				// Asigna las propiedades
					objPage.Brush = Mediator.BrushesRepository.LoadFirstBrush(objMLNode);
				// Carga los componentes de la página
					foreach (MLNode objMLChild in objMLNode.Nodes)
						switch (objMLChild.Name)
							{	case ComicRepositoryConstants.cnstStrTagImage:
										objPage.Content.Add(LoadContentImage(objPage, objComic, objMLChild));
									break;
								case ComicRepositoryConstants.cnstStrTagFrame:
										objPage.Content.Add(LoadFrame(objPage, objMLChild));
									break;
								case ComicRepositoryConstants.cnstStrTagBalloon:
										objPage.Content.Add(LoadFrame(objPage, objMLChild));
									break;
								case ComicRepositoryConstants.cnstStrTagText:
										objPage.Content.Add(LoadText(objPage, null, objMLChild));
									break;
								case ComicRepositoryConstants.cnstStrTagTimeLine:
										objPage.TimeLines.Add(Mediator.TimeLineRepository.LoadTimeLine(objPage, objMLChild));
									break;
							}
				// Añade la página al cómic
					objComic.Pages.Add(objPage);
		}

		/// <summary>
		///		Obtiene el contenido de una página
		/// </summary>
		internal ImageModel LoadContentImage(PageModel objPage, ComicModel objComic, MLNode objMLNode)
		{ ImageModel objImage = new ImageModel(objPage, objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value);
			string strFileName = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagFileName].Value;

				// Asigna las propiedades de la imagen
					objImage.ResourceKey = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagResourceKey].Value;
				// Asigna el nombre de archivo
					if (!strFileName.IsEmpty())
						objImage.FileName = System.IO.Path.Combine(objComic.Path, strFileName);
				// Asigna los atributos
					Mediator.CommonRepository.AssignAttributesPageItem(objMLNode, objImage);
				// Devuelve el contenido
					return objImage;
		}

		/// <summary>
		///		Carga un frame
		/// </summary>
		private FrameModel LoadFrame(PageModel objPage, MLNode objMLNode)
		{ FrameModel objFrame = new FrameModel(objPage, objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value);

				// Asigna las propiedades
					objFrame.Stretch = Mediator.CommonRepository.GetStretchMode(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagStretch].Value);
					objFrame.Brush = Mediator.BrushesRepository.LoadFirstBrush(objMLNode);
				// Asigna los radios
					objFrame.RadiusX = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagRadiusX].Value.GetDouble();
					objFrame.RadiusY = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagRadiusY].Value.GetDouble();
				// Asigna los atributos
					Mediator.CommonRepository.AssignAttributesPageItem(objMLNode, objFrame);
				// Obtiene los datos
					foreach (MLNode objMLChild in objMLNode.Nodes)
						switch (objMLChild.Name)
							{	case ComicRepositoryConstants.cnstStrTagPen:
										objFrame.Pen = LoadPen(objMLChild);
									break;
								case ComicRepositoryConstants.cnstStrTagShape:
										objFrame.Shape = Mediator.ShapesRepository.LoadShape(objPage, objMLChild);
									break;
								case ComicRepositoryConstants.cnstStrTagContent:
										LoadBalloonContent(objFrame, objMLChild);
									break;
								case ComicRepositoryConstants.cnstStrTagText:
										objFrame.Texts.Add(LoadText(objFrame.Page, objFrame, objMLChild));
									break;
							}
				// Devuelve el frame
					return objFrame;
		}

		/// <summary>
		///		Carga el contenido de un bocadillo 
		/// </summary>
		private void LoadBalloonContent(FrameModel objBalloon, MLNode objMLNode)
		{ foreach (MLNode objMLChild in objMLNode.Nodes)
				switch (objMLChild.Name)
					{	case ComicRepositoryConstants.cnstStrTagText:
								objBalloon.Texts.Add(LoadText(objBalloon.Page, objBalloon, objMLChild));
							break;
					}
		}

		/// <summary>
		///		Carga el contenido de un texto
		/// </summary>
		private TextModel LoadText(PageModel objPage, FrameModel objParent, MLNode objMLNode)
		{ TextModel objText = new TextModel(objPage, objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagKey].Value);

				// Asigna los parámetros básicos
					Mediator.CommonRepository.AssignAttributesPageItem(objMLNode, objText);
				// Obtiene los valores del nodo
					objText.IsBold = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagBold].Value.GetBool();
					objText.IsItalic = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagItalic].Value.GetBool();
					objText.FontName = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagFont].Value;
					objText.Size = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagSize].Value.GetDouble(10);
					objText.Color = new ColorModel(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagColor].Value);
				// Asigna el contenido
					if (objMLNode.Nodes.Count != 0)
						{ // Asigna el texto
								foreach (MLNode objMLChild in objMLNode.Nodes)
									switch (objMLChild.Name)
										{	case ComicRepositoryConstants.cnstStrTagContent:
													objText.Texts.Add(LoadText(objMLChild.Attributes[ComicRepositoryConstants.cnstStrTagLanguage].Value,
																										 objMLChild.Value));
												break;
											case ComicRepositoryConstants.cnstStrTagTransform:
													objText.Transforms.AddRange(Mediator.TransformRepository.LoadTransforms(objMLChild));
												break;
										}
						}
					else
						objText.Texts.Add(LoadText("", objMLNode.Value));
				// Asigna las dimensiones
					if (objParent != null)
						{ // Asigna el ancho
								if (objText.Dimensions.Width == null)
									objText.Dimensions.Width = objParent.Dimensions.WidthDefault - objText.Dimensions.LeftDefault;
							// Asigna el alto
								if (objText.Dimensions.Height == null)
									objText.Dimensions.Height = objParent.Dimensions.HeightDefault - objText.Dimensions.TopDefault;
						}
				// Devuelve el texto
					return objText;
		}

		/// <summary>
		///		Carga un texto de un idioma
		/// </summary>
		private TextContentModel LoadText(string strLanguageKey, string strText)
		{ return new TextContentModel(strLanguageKey, strText);
		}

		/// <summary>
		///		Carga los datos de un lápiz
		/// </summary>
		private PenModel LoadPen(MLNode objMLNode)
		{ PenModel objPen = new PenModel();

				// Asigna los valores
					objPen.Color = new ColorModel(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagColor].Value);
					objPen.Thickness = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagWidth].Value.GetDouble(1);
					objPen.Dots.AddRange(GetDots(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagDots].Value));
					objPen.CapDots = GetLineCap(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagCapDots].Value);
					objPen.StartLineCap = GetLineCap(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagStartLineCap].Value);
					objPen.EndLineCap = GetLineCap(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagEndLineCap].Value);
					objPen.JoinMode = GetLineJoin(objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagJoinMode].Value);
					objPen.DashOffset = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagDashOffset].Value.GetDouble();
					objPen.MiterLimit = objMLNode.Attributes[ComicRepositoryConstants.cnstStrTagMiterLimit].Value.GetDouble();
				// Devuelve el lápiz
					return objPen;
		}

		/// <summary>
		///		Obtiene el modo de unión de las líneas
		/// </summary>
		private PenModel.LineJoin GetLineJoin(string strValue)
		{	if (strValue.EqualsIgnoreCase("Bevel"))
				return PenModel.LineJoin.Bevel;
			else if (strValue.EqualsIgnoreCase("Round"))
				return PenModel.LineJoin.Round;
			else
				return PenModel.LineJoin.Miter;
		}

		/// <summary>
		///		Obtiene el modo de inicio / fin de las líneas
		/// </summary>
		private PenModel.LineCap GetLineCap(string strValue)
		{ if (strValue.EqualsIgnoreCase("Round"))
				return PenModel.LineCap.Round;
			else if (strValue.EqualsIgnoreCase("Square"))
				return PenModel.LineCap.Square;
			else if (strValue.EqualsIgnoreCase("Triangle"))
				return PenModel.LineCap.Triangle;
			else
				return PenModel.LineCap.Flat;
		}

		/// <summary>
		///		Obtiene los puntos de la línea
		/// </summary>
		private List<double> GetDots(string strValue)
		{ List<double> objColDots = new List<double>();

				// Carga los valores
					if (!strValue.IsEmpty())
						{ string [] arrStrValue = strValue.Split(',');

								foreach (string strDot in arrStrValue)
									{ double? dblDot = strDot.GetDouble();

											if (dblDot != null)
												objColDots.Add(dblDot ?? 0);
									}
						}
				// Devuelve la colección
					return objColDots;
		}

		/// <summary>
		///		Agregrador de repository
		/// </summary>
		private ComicReaderMediator Mediator { get; }
	}
}
