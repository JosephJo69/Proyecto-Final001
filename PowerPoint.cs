using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using A = DocumentFormat.OpenXml.Drawing;

namespace Proyecto001_WF
{
    public class PowerPoint
    {
        public void CrearDocumentoPowerPoint(string ruta, string texto)
        {
            using (PresentationDocument presentationDoc = PresentationDocument.Create(ruta, DocumentFormat.OpenXml.PresentationDocumentType.Presentation))
            {
                // Crear las partes principales
                PresentationPart presentationPart = presentationDoc.AddPresentationPart();
                presentationPart.Presentation = new Presentation();

                SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();
                slidePart.Slide = new Slide(new CommonSlideData(new ShapeTree()));

                // Crear el ShapeTree y el cuadro de texto
                var shapeTree = slidePart.Slide.GetFirstChild<CommonSlideData>().GetFirstChild<ShapeTree>();

                // Requerido: NonVisualDrawingProperties Id=1 para el cuadro de texto
                var shape = new Shape(
                    new NonVisualShapeProperties(
                        new NonVisualDrawingProperties() { Id = 1, Name = "Texto" },
                        new NonVisualShapeDrawingProperties(new A.ShapeLocks() { NoGrouping = true }),
                        new ApplicationNonVisualDrawingProperties()
                    ),
                    new ShapeProperties(
                        new A.Transform2D(
                            new A.Offset() { X = 914400, Y = 914400 }, // 1cm, 1cm
                            new A.Extents() { Cx = 6858000, Cy = 2000000 } // tamaño del cuadro
                        )
                    ),
                    new TextBody(
                        new A.BodyProperties(),
                        new A.ListStyle(),
                        new A.Paragraph(new A.Run(new A.Text(texto)))
                    )
                );
                shapeTree.AppendChild(shape);

                // Requerido: Agregar el cuadro de diapositiva (Background shape)
                shapeTree.AppendChild(new Picture(
                    new NonVisualPictureProperties(
                        new NonVisualDrawingProperties() { Id = 2, Name = "Fondo" },
                        new NonVisualPictureDrawingProperties(),
                        new ApplicationNonVisualDrawingProperties()
                    ),
                    new BlipFill(),
                    new ShapeProperties()
                ));

                // Relacionar la diapositiva con la presentación
                presentationPart.Presentation.AppendChild(new SlideIdList(
                    new SlideId() { Id = 256U, RelationshipId = presentationPart.GetIdOfPart(slidePart) }
                ));
                presentationPart.Presentation.Save();
            }
        }
    }
}
