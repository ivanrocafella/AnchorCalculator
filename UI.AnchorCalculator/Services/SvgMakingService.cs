using Core.AnchorCalculator.Entities;
using GrapeCity.Documents.Svg;
using System.Drawing;
using System.Text;

namespace UI.AnchorCalculator.Services
{
    public class SvgMakingService
    {
        public int X_InitCoord = 100; // X origin
        const int Y_InitCoord = 100; // Y origin
        public void GetSvg(Anchor anchor)
        {
            float ThreadStep = (float)anchor.ThreadStep; // parse to float double threadstep
            int lengthMax = 800; // max length of anchor
            int bendLengthMax = 300; // max length of anchor's bend

            if (anchor.BendLength <= bendLengthMax)
                X_InitCoord += anchor.BendLength; // X origin
            else
                X_InitCoord += bendLengthMax; // X origin

            int gap = 20; // gap in out of max length  of anchor
            int outPartHorSize = 30; // length output part of horizontal size
            int outPartRadSize = 45; // length of shelf of radius size

            var svgDoc = new GcSvgDocument();
            svgDoc.RootSvg.Width = new SvgLength(500, SvgLengthUnits.Pixels);
            svgDoc.RootSvg.Height = new SvgLength(500, SvgLengthUnits.Pixels);

            List<SvgElement> svgElements = new(); // Make list to fill with objects SvgRectElement

            //Draw part with thread

            var rectThreadBodyAnchor = GetSvgRectElement(X_InitCoord,
                Y_InitCoord,
                anchor.ThreadDiameter,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1.5f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadBodyAnchor);

            var rectThreadStepBodyAnchor = GetSvgRectElement(X_InitCoord + ThreadStep / 2,
                Y_InitCoord,
                anchor.ThreadDiameter - ThreadStep,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadStepBodyAnchor);

            // Draw sizes of part with thread

            // Size of thread's diametr

            var lineVertLeftSizeDiamThread = GetSvgLineElement(X_InitCoord,
                Y_InitCoord,
                X_InitCoord,
                Y_InitCoord - outPartHorSize,
                Color.Black,
                0.5f,
                SvgLengthUnits.Pixels);

            svgElements.Add(lineVertLeftSizeDiamThread);

            var lineVertRightSizeDiamThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                  Y_InitCoord,
                  X_InitCoord + anchor.ThreadDiameter,
                  Y_InitCoord - outPartHorSize,
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

            svgElements.Add(lineVertRightSizeDiamThread);

            var lineHorSizeDiamThread = GetSvgLineElement(X_InitCoord,
                       Y_InitCoord - (outPartHorSize - 5),
                       X_InitCoord + anchor.ThreadDiameter + 50,
                       Y_InitCoord - (outPartHorSize - 5),
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamThread);

            var lineSerifLeftSizeDiamThread = GetSerif(X_InitCoord,
                        Y_InitCoord - (outPartHorSize - 5),
                        X_InitCoord,
                        Y_InitCoord - (outPartHorSize - 5),
                        Color.Black,
                        0.5f,
                        SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamThread);

            var lineSerifRightSizeDiamThread = GetSerif(X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord - (outPartHorSize - 5),
                           X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord - (outPartHorSize - 5),
                           Color.Black,
                           0.5f,
                           SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamThread);


            svgElements.Add(GetSvgTextElement($"М{anchor.ThreadDiameter}x{ThreadStep}",
                            X_InitCoord + anchor.ThreadDiameter,
                            Y_InitCoord - (outPartHorSize - 5),
                            0,
                            SvgLengthUnits.Pixels));    // Make text of size's value diametr of thread

            // Size of thread's length

            var lineHorTopSizeLengthThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                      Y_InitCoord,
                      X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                      Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineHorTopSizeLengthThread);

            var lineHorBotSizeLengthThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord + anchor.ThreadLength,
                           X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                           Y_InitCoord + anchor.ThreadLength,
                           Color.Black,
                           0.5f,
                           SvgLengthUnits.Pixels);

            svgElements.Add(lineHorBotSizeLengthThread);

            var lineVerSizeDiamThread = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                 Y_InitCoord,
                                 X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                 Y_InitCoord + anchor.ThreadLength,
                                 Color.Black,
                                 0.5f,
                                 SvgLengthUnits.Pixels);

            svgElements.Add(lineVerSizeDiamThread);

            var lineSerifTopSizeDiamThread = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                             Y_InitCoord,
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                             Y_InitCoord,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifTopSizeDiamThread);

            var lineSerifBotSizeDiamThread = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                  Y_InitCoord + anchor.ThreadLength,
                                  X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                  Y_InitCoord + anchor.ThreadLength,
                                  Color.Black,
                                  0.5f,
                                  SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifBotSizeDiamThread);

            svgElements.Add(GetSvgTextElement($"{anchor.ThreadLength}",
                X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                Y_InitCoord + anchor.ThreadLength / 2 + 10,
                -90,
                SvgLengthUnits.Pixels));    // Make text of size's value length of thread

            // Size of anchors's diametr

            var lineHorSizeDiamAnchor = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                       X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 40,
                       Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamAnchor);

            var lineSerifLeftSizeDiamAnchor = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamAnchor);

            var lineSerifRightSizeDiamAnchor = GetSerif(X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                     X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamAnchor);

            svgElements.Add(GetSvgTextElement($"d{anchor.Diameter}",
                X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 5,
                Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                0,
                SvgLengthUnits.Pixels));    // Make text of size's value diametr of anchor

            //Make object basic part without thread and bend 

            SvgRectElement rectBasicBodyAnchor;

            //Make objects of sizes anchor's length

            SvgLineElement lineHorTopSizeLengthOfAnchor;
            SvgLineElement lineHorBotSizeLengthOfAnchor;
            SvgLineElement lineVertSizeLengthOfAnchor;

            //Make object bending part without radius

            SvgRectElement rectBendAnchor;

            //Make object bending part with radius

            var pbRadiusBend = new SvgPathBuilder();
            var pathRadiusBend = new SvgPathElement();

            if (anchor.Length <= lengthMax)
            {
                if (anchor.BendLength > anchor.Diameter + anchor.BendRadius)
                {
                    //Draw basic part without thread and bend 

                    rectBasicBodyAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + anchor.ThreadLength,
                        anchor.Diameter,
                        anchor.Length - (anchor.ThreadLength + anchor.BendRadius + anchor.Diameter),
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);

                    if (anchor.BendLength <= bendLengthMax)
                    {
                        //Draw bending part without radius

                        rectBendAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - (anchor.BendLength - anchor.Diameter),
                            Y_InitCoord + anchor.Length - anchor.Diameter,
                            anchor.BendLength - (anchor.BendRadius + anchor.Diameter),
                            anchor.Diameter,
                            Color.Transparent,
                            Color.Black,
                            1.5f,
                            SvgLengthUnits.Pixels);

                        svgElements.Add(rectBendAnchor);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + anchor.Length,
                              X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                              Y_InitCoord + anchor.Length + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread
                    }
                    else
                    {
                        //Draw bending part without radius 

                        //Make right half bending part without radius

                        var pbHalfRightBendPartAnchor = new SvgPathBuilder();
                        var pathHalfRightBendPartAnchor = new SvgPathElement();

                        pbHalfRightBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap,
                            Y_InitCoord + anchor.Length - anchor.Diameter);
                        pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius);
                        pbHalfRightBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.Length);
                        pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap);

                        pathHalfRightBendPartAnchor.PathData = pbHalfRightBendPartAnchor.ToPathData();
                        pathHalfRightBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathHalfRightBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                        pathHalfRightBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                        svgElements.Add(pathHalfRightBendPartAnchor);

                        //Make left half bending part without radius

                        var pbHalfLeftBendPartAnchor = new SvgPathBuilder();
                        var pathHalfLeftBendPartAnchor = new SvgPathElement();

                        pbHalfLeftBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2,
                            Y_InitCoord + anchor.Length - anchor.Diameter);
                        pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax);
                        pbHalfLeftBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.Length);
                        pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2);

                        pathHalfLeftBendPartAnchor.PathData = pbHalfLeftBendPartAnchor.ToPathData();
                        pathHalfLeftBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathHalfLeftBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                        pathHalfLeftBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                        svgElements.Add(pathHalfLeftBendPartAnchor);

                        // Make gap Right Line

                        var pbgapRight = new SvgPathBuilder();
                        var pathgapRight = new SvgPathElement();

                        pbgapRight.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap,
                               Y_InitCoord + anchor.Length - anchor.Diameter);
                        pbgapRight.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap - 5,
                            Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap,
                            Y_InitCoord + anchor.Length,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap,
                            Y_InitCoord + anchor.Length);

                        pathgapRight.PathData = pbgapRight.ToPathData();
                        pathgapRight.Fill = new SvgPaint(Color.Transparent);
                        pathgapRight.Stroke = new SvgPaint(Color.Black);
                        pathgapRight.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapRight);

                        // Make gap Left Line

                        var pbgapLeft = new SvgPathBuilder();
                        var pathgapLeft = new SvgPathElement();

                        pbgapLeft.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2,
                            Y_InitCoord + anchor.Length - anchor.Diameter);
                        pbgapLeft.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 - 5,
                            Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2,
                            Y_InitCoord + anchor.Length,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2,
                            Y_InitCoord + anchor.Length);

                        pathgapLeft.PathData = pbgapLeft.ToPathData();
                        pathgapLeft.Fill = new SvgPaint(Color.Transparent);
                        pathgapLeft.Stroke = new SvgPaint(Color.Black);
                        pathgapLeft.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapLeft);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + anchor.Length,
                              X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                              Y_InitCoord + anchor.Length + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread
                    }

                    //Draw bending part with radius 

                    pbRadiusBend.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter));
                    pbRadiusBend.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + anchor.Length - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + anchor.Length - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + anchor.Length - anchor.Diameter);
                    pbRadiusBend.AddVerticalLineTo(false, Y_InitCoord + anchor.Length);
                    pbRadiusBend.AddCurveTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + anchor.Length,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                        X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter) / 2,
                        Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter));


                    pathRadiusBend.PathData = pbRadiusBend.ToPathData();
                    pathRadiusBend.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBend.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBend.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBend);

                    //Size of radius

                    var lineInclinSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                         Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                         X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                         Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineInclinSizeRadius);

                    var lineHorSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                      Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                      Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeRadius);

                    var lineSerifSizeRadius = GetSerifRad(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                            Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                            Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifSizeRadius);

                    svgElements.Add(GetSvgTextElement($"R{anchor.BendRadius}",
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                        Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                        0,
                        SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor
                }
                else
                {
                    rectBasicBodyAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + anchor.ThreadLength,
                        anchor.Diameter,
                        anchor.Length - anchor.ThreadLength,
                        Color.Transparent,
                        Color.Black,
                        1,
                        SvgLengthUnits.Pixels);
                }

                svgElements.Add(rectBasicBodyAnchor);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);

                if (anchor.BendLength > anchor.Diameter + anchor.BendRadius)
                {
                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                     Y_InitCoord + anchor.Length,
                                     X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                     Y_InitCoord + anchor.Length,
                                     Color.Black,
                                     0.5f,
                                     SvgLengthUnits.Pixels);
                }
                else
                {
                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                  Y_InitCoord + anchor.Length,
                                  X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                  Y_InitCoord + anchor.Length,
                                  Color.Black,
                                  0.5f,
                                  SvgLengthUnits.Pixels);
                }

                svgElements.Add(lineHorBotSizeLengthOfAnchor);


                lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);

                var lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                          Y_InitCoord + anchor.Length / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor
            }
            else
            {
                //Draw basic part without thread and bend 

                //Make top half basic part without thread and bend

                var pbHalfTopBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfTopBasicBodyAnchor = new SvgPathElement();

                pbHalfTopBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength);
                pbHalfTopBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                pathHalfTopBasicBodyAnchor.PathData = pbHalfTopBasicBodyAnchor.ToPathData();
                pathHalfTopBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                pathHalfTopBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                pathHalfTopBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathHalfTopBasicBodyAnchor);

                // Make gap Top Line

                var pbgapTop = new SvgPathBuilder();
                var pathgapTop = new SvgPathElement();

                pbgapTop.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                pbgapTop.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap) - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap),
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                pathgapTop.PathData = pbgapTop.ToPathData();
                pathgapTop.Fill = new SvgPaint(Color.Transparent);
                pathgapTop.Stroke = new SvgPaint(Color.Black);
                pathgapTop.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapTop);

                SvgLineElement lineSerifBotSizeLengthOfAnchor;

                // Make gap Bot Line

                var pbgapBot = new SvgPathBuilder();
                var pathgapBot = new SvgPathElement();

                pbgapBot.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                pbgapBot.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2 - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                pathgapBot.PathData = pbgapBot.ToPathData();
                pathgapBot.Fill = new SvgPaint(Color.Transparent);
                pathgapBot.Stroke = new SvgPaint(Color.Black);
                pathgapBot.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapBot);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);


                var pbHalfBotBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfBotBasicBodyAnchor = new SvgPathElement();

                if (anchor.BendLength > anchor.Diameter + anchor.BendRadius)
                {
                    //Make bottom half basic part without thread and bend

                    pbHalfBotBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter);
                    pbHalfBotBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodyAnchor.PathData = pbHalfBotBasicBodyAnchor.ToPathData();
                    pathHalfBotBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodyAnchor);

                    //Draw bending part with radius 

                    pbRadiusBend.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter);
                    pbRadiusBend.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                    pbRadiusBend.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax);
                    pbRadiusBend.AddCurveTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter,
                        X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter);

                    pathRadiusBend.PathData = pbRadiusBend.ToPathData();
                    pathRadiusBend.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBend.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBend.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBend);

                    if (anchor.BendLength <= bendLengthMax)
                    {
                        //Draw bending part without radius

                        rectBendAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - (anchor.BendLength - anchor.Diameter),
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter,
                            anchor.BendLength - (anchor.BendRadius + anchor.Diameter),
                            anchor.Diameter,
                            Color.Transparent,
                            Color.Black,
                            1.5f,
                            SvgLengthUnits.Pixels);

                        svgElements.Add(rectBendAnchor);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + anchor.ThreadLength + lengthMax,
                              X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + anchor.ThreadLength + lengthMax + (outPartHorSize + 5),
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter,
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.ThreadLength + lengthMax + (outPartHorSize + 5),
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                              Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread
                    }
                    else
                    {
                        //Draw bending part without radius 

                        //Make right half bending part without radius

                        var pbHalfRightBendPartAnchor = new SvgPathBuilder();
                        var pathHalfRightBendPartAnchor = new SvgPathElement();

                        pbHalfRightBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                        pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius);
                        pbHalfRightBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + anchor.ThreadLength);
                        pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap);

                        pathHalfRightBendPartAnchor.PathData = pbHalfRightBendPartAnchor.ToPathData();
                        pathHalfRightBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathHalfRightBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                        pathHalfRightBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                        svgElements.Add(pathHalfRightBendPartAnchor);

                        //Make left half bending part without radius

                        var pbHalfLeftBendPartAnchor = new SvgPathBuilder();
                        var pathHalfLeftBendPartAnchor = new SvgPathElement();

                        pbHalfLeftBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                        pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax);
                        pbHalfLeftBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + anchor.ThreadLength);
                        pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2);

                        pathHalfLeftBendPartAnchor.PathData = pbHalfLeftBendPartAnchor.ToPathData();
                        pathHalfLeftBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathHalfLeftBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                        pathHalfLeftBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                        svgElements.Add(pathHalfLeftBendPartAnchor);

                        // Make gap Right Line

                        var pbgapRight = new SvgPathBuilder();
                        var pathgapRight = new SvgPathElement();

                        pbgapRight.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap,
                               Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                        pbgapRight.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap - 5,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap,
                            Y_InitCoord + lengthMax + anchor.ThreadLength,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 + gap,
                            Y_InitCoord + lengthMax + anchor.ThreadLength);

                        pathgapRight.PathData = pbgapRight.ToPathData();
                        pathgapRight.Fill = new SvgPaint(Color.Transparent);
                        pathgapRight.Stroke = new SvgPaint(Color.Black);
                        pathgapRight.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapRight);

                        // Make gap Left Line

                        var pbgapLeft = new SvgPathBuilder();
                        var pathgapLeft = new SvgPathElement();

                        pbgapLeft.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                        pbgapLeft.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2 - 5,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + anchor.ThreadLength,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + anchor.ThreadLength);

                        pathgapLeft.PathData = pbgapLeft.ToPathData();
                        pathgapLeft.Fill = new SvgPaint(Color.Transparent);
                        pathgapLeft.Stroke = new SvgPaint(Color.Black);
                        pathgapLeft.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapLeft);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + lengthMax + anchor.ThreadLength,
                              X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + lengthMax + anchor.ThreadLength + (outPartHorSize + 5),
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + lengthMax + anchor.ThreadLength - (anchor.BendRadius + anchor.Diameter),
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + lengthMax + anchor.ThreadLength + (outPartHorSize + 5),
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                              Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread
                    }

                    //Size of radius

                    var lineInclinSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                         Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                         X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                         Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineInclinSizeRadius);

                    var lineHorSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                      Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                      Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeRadius);

                    var lineSerifSizeRadius = GetSerifRad(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                      Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                      Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifSizeRadius);

                    svgElements.Add(GetSvgTextElement($"R{anchor.BendRadius}",
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                        0,
                        SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor

                    //Make bottom part anchor's length

                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                    Y_InitCoord + anchor.ThreadLength + lengthMax,
                                    X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                    Y_InitCoord + anchor.ThreadLength + lengthMax,
                                    Color.Black,
                                    0.5f,
                                    SvgLengthUnits.Pixels);

                    lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.ThreadLength + lengthMax,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.ThreadLength + lengthMax,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.ThreadLength + lengthMax,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                    svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                          Y_InitCoord + (anchor.ThreadLength + lengthMax) / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor
                }
                else
                {
                    //Make bottom half basic part without thread and bend

                    pbHalfBotBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax);
                    pbHalfBotBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodyAnchor.PathData = pbHalfBotBasicBodyAnchor.ToPathData();
                    pathHalfBotBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodyAnchor);

                    // Make size anchors length 

                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                  Y_InitCoord + anchor.ThreadLength + lengthMax,
                                  X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                  Y_InitCoord + anchor.ThreadLength + lengthMax,
                                  Color.Black,
                                  0.5f,
                                  SvgLengthUnits.Pixels);

                    lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord + anchor.ThreadLength + lengthMax,
                           X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord + anchor.ThreadLength + lengthMax,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

                    lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord,
                       X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord + anchor.ThreadLength + lengthMax,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

                    svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                         Y_InitCoord + (anchor.ThreadLength + lengthMax) / 2 + 10,
                         -90,
                         SvgLengthUnits.Pixels));    // Make text of size's value length of anchor

                }

                svgElements.Add(lineHorBotSizeLengthOfAnchor);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);
            }

          // GetDescriptionAnchor(anchor, paramsCanvas, svgElements); 

            for (int i = 0; i < svgElements.Count; i++)
                svgDoc.RootSvg.Children.Insert(i, svgElements[i]);

            SvgViewBox view = new();
            view.MinX = 0;
            view.MinY = 0;
            view.Width = 900;
            view.Height = 1100;

            svgDoc.RootSvg.ViewBox = view;

            StringBuilder stringBuilder = new();
            svgDoc.Save(stringBuilder);
            string xml = stringBuilder.ToString();
            string svgElem = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            anchor.SvgElement = svgElem;
        }

        public void GetSvgStraightAnchor(Anchor anchor)
        {
            float ThreadStep = (float)anchor.ThreadStep; // parse to float double threadstep
            int lengthMax = 800; // max length of anchor
            int bendLengthMax = 300; // max length of anchor's bend

            if (anchor.BendLength <= bendLengthMax)
                X_InitCoord += anchor.BendLength; // X origin
            else
                X_InitCoord += bendLengthMax; // X origin

            int gap = 20; // gap in out of max length  of anchor
            int outPartHorSize = 30; // length output part of horizontal size
            int outPartRadSize = 45; // length of shelf of radius size

            var svgDoc = new GcSvgDocument();
            svgDoc.RootSvg.Width = new SvgLength(500, SvgLengthUnits.Pixels);
            svgDoc.RootSvg.Height = new SvgLength(500, SvgLengthUnits.Pixels);

            List<SvgElement> svgElements = new(); // Make list to fill with objects SvgRectElement

            //Draw part with thread

            var rectThreadBodyAnchor = GetSvgRectElement(X_InitCoord,
                Y_InitCoord,
                anchor.ThreadDiameter,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1.5f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadBodyAnchor);

            var rectThreadStepBodyAnchor = GetSvgRectElement(X_InitCoord + ThreadStep / 2,
                Y_InitCoord,
                anchor.ThreadDiameter - ThreadStep,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadStepBodyAnchor);

            // Draw sizes of part with thread

            // Size of thread's diametr

            var lineVertLeftSizeDiamThread = GetSvgLineElement(X_InitCoord,
                Y_InitCoord,
                X_InitCoord,
                Y_InitCoord - outPartHorSize,
                Color.Black,
                0.5f,
                SvgLengthUnits.Pixels);

            svgElements.Add(lineVertLeftSizeDiamThread);

            var lineVertRightSizeDiamThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                  Y_InitCoord,
                  X_InitCoord + anchor.ThreadDiameter,
                  Y_InitCoord - outPartHorSize,
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

            svgElements.Add(lineVertRightSizeDiamThread);

            var lineHorSizeDiamThread = GetSvgLineElement(X_InitCoord,
                       Y_InitCoord - (outPartHorSize - 5),
                       X_InitCoord + anchor.ThreadDiameter + 50,
                       Y_InitCoord - (outPartHorSize - 5),
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamThread);

            var lineSerifLeftSizeDiamThread = GetSerif(X_InitCoord,
                        Y_InitCoord - (outPartHorSize - 5),
                        X_InitCoord,
                        Y_InitCoord - (outPartHorSize - 5),
                        Color.Black,
                        0.5f,
                        SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamThread);

            var lineSerifRightSizeDiamThread = GetSerif(X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord - (outPartHorSize - 5),
                           X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord - (outPartHorSize - 5),
                           Color.Black,
                           0.5f,
                           SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamThread);


            svgElements.Add(GetSvgTextElement($"М{anchor.ThreadDiameter}x{ThreadStep}",
                            X_InitCoord + anchor.ThreadDiameter,
                            Y_InitCoord - (outPartHorSize - 5),
                            0,
                            SvgLengthUnits.Pixels));    // Make text of size's value diametr of thread

            // Size of thread's length

            var lineHorTopSizeLengthThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                      Y_InitCoord,
                      X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                      Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineHorTopSizeLengthThread);

            var lineHorBotSizeLengthThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord + anchor.ThreadLength,
                           X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                           Y_InitCoord + anchor.ThreadLength,
                           Color.Black,
                           0.5f,
                           SvgLengthUnits.Pixels);

            svgElements.Add(lineHorBotSizeLengthThread);

            var lineVerSizeDiamThread = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                 Y_InitCoord,
                                 X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                 Y_InitCoord + anchor.ThreadLength,
                                 Color.Black,
                                 0.5f,
                                 SvgLengthUnits.Pixels);

            svgElements.Add(lineVerSizeDiamThread);

            var lineSerifTopSizeDiamThread = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                             Y_InitCoord,
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                             Y_InitCoord,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifTopSizeDiamThread);

            var lineSerifBotSizeDiamThread = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                  Y_InitCoord + anchor.ThreadLength,
                                  X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                  Y_InitCoord + anchor.ThreadLength,
                                  Color.Black,
                                  0.5f,
                                  SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifBotSizeDiamThread);

            svgElements.Add(GetSvgTextElement($"{anchor.ThreadLength}",
                X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                Y_InitCoord + anchor.ThreadLength / 2 + 10,
                -90,
                SvgLengthUnits.Pixels));    // Make text of size's value length of thread

            // Size of anchors's diametr

            var lineHorSizeDiamAnchor = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                       X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 40,
                       Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamAnchor);

            var lineSerifLeftSizeDiamAnchor = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamAnchor);

            var lineSerifRightSizeDiamAnchor = GetSerif(X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                     X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamAnchor);

            svgElements.Add(GetSvgTextElement($"d{anchor.Diameter}",
                X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 5,
                Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                0,
                SvgLengthUnits.Pixels));    // Make text of size's value diametr of anchor

            //Make object basic part without thread and bend 

            SvgRectElement rectBasicBodyAnchor;

            //Make objects of sizes anchor's length

            SvgLineElement lineHorTopSizeLengthOfAnchor;
            SvgLineElement lineHorBotSizeLengthOfAnchor;
            SvgLineElement lineVertSizeLengthOfAnchor;

            //Make object bending part without radius

            SvgRectElement rectBendAnchor;

            //Make object bending part with radius

            var pbRadiusBend = new SvgPathBuilder();
            var pathRadiusBend = new SvgPathElement();

            if (anchor.Length <= lengthMax)
            {
    
                rectBasicBodyAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + anchor.ThreadLength,
                    anchor.Diameter,
                    anchor.Length - anchor.ThreadLength,
                    Color.Transparent,
                    Color.Black,
                    1,
                    SvgLengthUnits.Pixels);

                svgElements.Add(rectBasicBodyAnchor);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);
           
                lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                              Y_InitCoord + anchor.Length,
                              X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                              Y_InitCoord + anchor.Length,
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);
             

                svgElements.Add(lineHorBotSizeLengthOfAnchor);


                lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);

                var lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                          Y_InitCoord + anchor.Length / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor
            }
            else
            {
                //Draw basic part without thread and bend 

                //Make top half basic part without thread and bend

                var pbHalfTopBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfTopBasicBodyAnchor = new SvgPathElement();

                pbHalfTopBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength);
                pbHalfTopBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                pathHalfTopBasicBodyAnchor.PathData = pbHalfTopBasicBodyAnchor.ToPathData();
                pathHalfTopBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                pathHalfTopBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                pathHalfTopBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathHalfTopBasicBodyAnchor);

                // Make gap Top Line

                var pbgapTop = new SvgPathBuilder();
                var pathgapTop = new SvgPathElement();

                pbgapTop.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                pbgapTop.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap) - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap),
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                pathgapTop.PathData = pbgapTop.ToPathData();
                pathgapTop.Fill = new SvgPaint(Color.Transparent);
                pathgapTop.Stroke = new SvgPaint(Color.Black);
                pathgapTop.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapTop);

                SvgLineElement lineSerifBotSizeLengthOfAnchor;

                // Make gap Bot Line

                var pbgapBot = new SvgPathBuilder();
                var pathgapBot = new SvgPathElement();

                pbgapBot.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                pbgapBot.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2 - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                pathgapBot.PathData = pbgapBot.ToPathData();
                pathgapBot.Fill = new SvgPaint(Color.Transparent);
                pathgapBot.Stroke = new SvgPaint(Color.Black);
                pathgapBot.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapBot);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);


                var pbHalfBotBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfBotBasicBodyAnchor = new SvgPathElement();

                    //Make bottom half basic part without thread and bend

                    pbHalfBotBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax);
                    pbHalfBotBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodyAnchor.PathData = pbHalfBotBasicBodyAnchor.ToPathData();
                    pathHalfBotBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodyAnchor);

                    // Make size anchors length 

                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                  Y_InitCoord + anchor.ThreadLength + lengthMax,
                                  X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                  Y_InitCoord + anchor.ThreadLength + lengthMax,
                                  Color.Black,
                                  0.5f,
                                  SvgLengthUnits.Pixels);

                    lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord + anchor.ThreadLength + lengthMax,
                           X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord + anchor.ThreadLength + lengthMax,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

                    lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord,
                       X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord + anchor.ThreadLength + lengthMax,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

                    svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                         Y_InitCoord + (anchor.ThreadLength + lengthMax) / 2 + 10,
                         -90,
                         SvgLengthUnits.Pixels));    // Make text of size's value length of anchor

 

                svgElements.Add(lineHorBotSizeLengthOfAnchor);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);
            }

          // GetDescriptionAnchor(anchor, paramsCanvas, svgElements); 

            for (int i = 0; i < svgElements.Count; i++)
                svgDoc.RootSvg.Children.Insert(i, svgElements[i]);

            SvgViewBox view = new();
            view.MinX = 0;
            view.MinY = 0;
            view.Width = 900;
            view.Height = 1100;

            svgDoc.RootSvg.ViewBox = view;

            StringBuilder stringBuilder = new();
            svgDoc.Save(stringBuilder);
            string xml = stringBuilder.ToString();
            string svgElem = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            anchor.SvgElement = svgElem;
        }

        public void GetSvgBendAnchor(Anchor anchor)
        {
            float ThreadStep = (float)anchor.ThreadStep; // parse to float double threadstep
            int lengthMax = 800; // max length of anchor
            int bendLengthMax = 300; // max length of anchor's bend

            if (anchor.BendLength <= bendLengthMax)
                X_InitCoord += anchor.BendLength; // X origin
            else
                X_InitCoord += bendLengthMax; // X origin

            int gap = 20; // gap in out of max length  of anchor
            int outPartHorSize = 30; // length output part of horizontal size
            int outPartRadSize = 45; // length of shelf of radius size

            var svgDoc = new GcSvgDocument();
            svgDoc.RootSvg.Width = new SvgLength(500, SvgLengthUnits.Pixels);
            svgDoc.RootSvg.Height = new SvgLength(500, SvgLengthUnits.Pixels);

            List<SvgElement> svgElements = new(); // Make list to fill with objects SvgRectElement

            //Draw part with thread

            var rectThreadBodyAnchor = GetSvgRectElement(X_InitCoord,
                Y_InitCoord,
                anchor.ThreadDiameter,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1.5f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadBodyAnchor);

            var rectThreadStepBodyAnchor = GetSvgRectElement(X_InitCoord + ThreadStep / 2,
                Y_InitCoord,
                anchor.ThreadDiameter - ThreadStep,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadStepBodyAnchor);

            // Draw sizes of part with thread

            // Size of thread's diametr

            var lineVertLeftSizeDiamThread = GetSvgLineElement(X_InitCoord,
                Y_InitCoord,
                X_InitCoord,
                Y_InitCoord - outPartHorSize,
                Color.Black,
                0.5f,
                SvgLengthUnits.Pixels);

            svgElements.Add(lineVertLeftSizeDiamThread);

            var lineVertRightSizeDiamThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                  Y_InitCoord,
                  X_InitCoord + anchor.ThreadDiameter,
                  Y_InitCoord - outPartHorSize,
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

            svgElements.Add(lineVertRightSizeDiamThread);

            var lineHorSizeDiamThread = GetSvgLineElement(X_InitCoord,
                       Y_InitCoord - (outPartHorSize - 5),
                       X_InitCoord + anchor.ThreadDiameter + 50,
                       Y_InitCoord - (outPartHorSize - 5),
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamThread);

            var lineSerifLeftSizeDiamThread = GetSerif(X_InitCoord,
                        Y_InitCoord - (outPartHorSize - 5),
                        X_InitCoord,
                        Y_InitCoord - (outPartHorSize - 5),
                        Color.Black,
                        0.5f,
                        SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamThread);

            var lineSerifRightSizeDiamThread = GetSerif(X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord - (outPartHorSize - 5),
                           X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord - (outPartHorSize - 5),
                           Color.Black,
                           0.5f,
                           SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamThread);


            svgElements.Add(GetSvgTextElement($"М{anchor.ThreadDiameter}x{ThreadStep}",
                            X_InitCoord + anchor.ThreadDiameter,
                            Y_InitCoord - (outPartHorSize - 5),
                            0,
                            SvgLengthUnits.Pixels));    // Make text of size's value diametr of thread

            // Size of thread's length

            var lineHorTopSizeLengthThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                      Y_InitCoord,
                      X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                      Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineHorTopSizeLengthThread);

            var lineHorBotSizeLengthThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord + anchor.ThreadLength,
                           X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                           Y_InitCoord + anchor.ThreadLength,
                           Color.Black,
                           0.5f,
                           SvgLengthUnits.Pixels);

            svgElements.Add(lineHorBotSizeLengthThread);

            var lineVerSizeDiamThread = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                 Y_InitCoord,
                                 X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                 Y_InitCoord + anchor.ThreadLength,
                                 Color.Black,
                                 0.5f,
                                 SvgLengthUnits.Pixels);

            svgElements.Add(lineVerSizeDiamThread);

            var lineSerifTopSizeDiamThread = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                             Y_InitCoord,
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                             Y_InitCoord,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifTopSizeDiamThread);

            var lineSerifBotSizeDiamThread = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                  Y_InitCoord + anchor.ThreadLength,
                                  X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                  Y_InitCoord + anchor.ThreadLength,
                                  Color.Black,
                                  0.5f,
                                  SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifBotSizeDiamThread);

            svgElements.Add(GetSvgTextElement($"{anchor.ThreadLength}",
                X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                Y_InitCoord + anchor.ThreadLength / 2 + 10,
                -90,
                SvgLengthUnits.Pixels));    // Make text of size's value length of thread

            // Size of anchors's diametr

            var lineHorSizeDiamAnchor = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                       X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 40,
                       Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamAnchor);

            var lineSerifLeftSizeDiamAnchor = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamAnchor);

            var lineSerifRightSizeDiamAnchor = GetSerif(X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                     X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamAnchor);

            svgElements.Add(GetSvgTextElement($"d{anchor.Diameter}",
                X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 5,
                Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                0,
                SvgLengthUnits.Pixels));    // Make text of size's value diametr of anchor

            //Make object basic part without thread and bend 

            SvgRectElement rectBasicBodyAnchor;

            //Make objects of sizes anchor's length

            SvgLineElement lineHorTopSizeLengthOfAnchor;
            SvgLineElement lineHorBotSizeLengthOfAnchor;
            SvgLineElement lineVertSizeLengthOfAnchor;

            //Make object bending part without radius

            SvgRectElement rectBendAnchor;

            //Make object bending part with radius

            var pbRadiusBend = new SvgPathBuilder();
            var pathRadiusBend = new SvgPathElement();

            if (anchor.Length <= lengthMax)
            {
              
                    //Draw basic part without thread and bend 

                    rectBasicBodyAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + anchor.ThreadLength,
                        anchor.Diameter,
                        anchor.Length - (anchor.ThreadLength + anchor.BendRadius + anchor.Diameter),
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);

                    if (anchor.BendLength <= bendLengthMax)
                    {
                        //Draw bending part without radius

                        rectBendAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - (anchor.BendLength - anchor.Diameter),
                            Y_InitCoord + anchor.Length - anchor.Diameter,
                            anchor.BendLength - (anchor.BendRadius + anchor.Diameter),
                            anchor.Diameter,
                            Color.Transparent,
                            Color.Black,
                            1.5f,
                            SvgLengthUnits.Pixels);

                        svgElements.Add(rectBendAnchor);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + anchor.Length,
                              X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                              Y_InitCoord + anchor.Length + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread
                    }
                    else
                    {
                        //Draw bending part without radius 

                        //Make right half bending part without radius

                        var pbHalfRightBendPartAnchor = new SvgPathBuilder();
                        var pathHalfRightBendPartAnchor = new SvgPathElement();

                        pbHalfRightBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                            Y_InitCoord + anchor.Length - anchor.Diameter);
                        pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius);
                        pbHalfRightBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.Length);
                        pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap);

                        pathHalfRightBendPartAnchor.PathData = pbHalfRightBendPartAnchor.ToPathData();
                        pathHalfRightBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathHalfRightBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                        pathHalfRightBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                        svgElements.Add(pathHalfRightBendPartAnchor);

                        //Make left half bending part without radius

                        var pbHalfLeftBendPartAnchor = new SvgPathBuilder();
                        var pathHalfLeftBendPartAnchor = new SvgPathElement();

                        pbHalfLeftBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + anchor.Length - anchor.Diameter);
                        pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax);
                        pbHalfLeftBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.Length);
                        pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2);

                        pathHalfLeftBendPartAnchor.PathData = pbHalfLeftBendPartAnchor.ToPathData();
                        pathHalfLeftBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathHalfLeftBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                        pathHalfLeftBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                        svgElements.Add(pathHalfLeftBendPartAnchor);

                        // Make gap Right Line

                        var pbgapRight = new SvgPathBuilder();
                        var pathgapRight = new SvgPathElement();

                        pbgapRight.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                               Y_InitCoord + anchor.Length - anchor.Diameter);
                        pbgapRight.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap - 5,
                            Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                            Y_InitCoord + anchor.Length,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                            Y_InitCoord + anchor.Length);

                        pathgapRight.PathData = pbgapRight.ToPathData();
                        pathgapRight.Fill = new SvgPaint(Color.Transparent);
                        pathgapRight.Stroke = new SvgPaint(Color.Black);
                        pathgapRight.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapRight);

                        // Make gap Left Line

                        var pbgapLeft = new SvgPathBuilder();
                        var pathgapLeft = new SvgPathElement();

                        pbgapLeft.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + anchor.Length - anchor.Diameter);
                        pbgapLeft.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 - 5,
                            Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + anchor.Length,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + anchor.Length);

                        pathgapLeft.PathData = pbgapLeft.ToPathData();
                        pathgapLeft.Fill = new SvgPaint(Color.Transparent);
                        pathgapLeft.Stroke = new SvgPaint(Color.Black);
                        pathgapLeft.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapLeft);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + anchor.Length,
                              X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                              Y_InitCoord + anchor.Length + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread
                    }

                    //Draw bending part with radius 

                    pbRadiusBend.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter));
                    pbRadiusBend.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + anchor.Length - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + anchor.Length - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + anchor.Length - anchor.Diameter);
                    pbRadiusBend.AddVerticalLineTo(false, Y_InitCoord + anchor.Length);
                    pbRadiusBend.AddCurveTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + anchor.Length,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                        X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter) / 2,
                        Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter));


                    pathRadiusBend.PathData = pbRadiusBend.ToPathData();
                    pathRadiusBend.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBend.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBend.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBend);

                    //Size of radius

                    var lineInclinSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                         Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                         X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                         Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineInclinSizeRadius);

                    var lineHorSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                      Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                      Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeRadius);

                    var lineSerifSizeRadius = GetSerifRad(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                            Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                            Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifSizeRadius);

                    svgElements.Add(GetSvgTextElement($"R{anchor.BendRadius}",
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                        Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                        0,
                        SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor
                
              

                svgElements.Add(rectBasicBodyAnchor);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);

           
                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                     Y_InitCoord + anchor.Length,
                                     X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                     Y_InitCoord + anchor.Length,
                                     Color.Black,
                                     0.5f,
                                     SvgLengthUnits.Pixels);
                
              

                svgElements.Add(lineHorBotSizeLengthOfAnchor);


                lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);

                var lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                          Y_InitCoord + anchor.Length / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor
            }
            else
            {
                //Draw basic part without thread and bend 

                //Make top half basic part without thread and bend

                var pbHalfTopBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfTopBasicBodyAnchor = new SvgPathElement();

                pbHalfTopBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength);
                pbHalfTopBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                pathHalfTopBasicBodyAnchor.PathData = pbHalfTopBasicBodyAnchor.ToPathData();
                pathHalfTopBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                pathHalfTopBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                pathHalfTopBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathHalfTopBasicBodyAnchor);

                // Make gap Top Line

                var pbgapTop = new SvgPathBuilder();
                var pathgapTop = new SvgPathElement();

                pbgapTop.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                pbgapTop.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap) - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap),
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                pathgapTop.PathData = pbgapTop.ToPathData();
                pathgapTop.Fill = new SvgPaint(Color.Transparent);
                pathgapTop.Stroke = new SvgPaint(Color.Black);
                pathgapTop.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapTop);

                SvgLineElement lineSerifBotSizeLengthOfAnchor;

                // Make gap Bot Line

                var pbgapBot = new SvgPathBuilder();
                var pathgapBot = new SvgPathElement();

                pbgapBot.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                pbgapBot.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2 - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                pathgapBot.PathData = pbgapBot.ToPathData();
                pathgapBot.Fill = new SvgPaint(Color.Transparent);
                pathgapBot.Stroke = new SvgPaint(Color.Black);
                pathgapBot.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapBot);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);


                var pbHalfBotBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfBotBasicBodyAnchor = new SvgPathElement();

              
                    //Make bottom half basic part without thread and bend

                    pbHalfBotBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter);
                    pbHalfBotBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodyAnchor.PathData = pbHalfBotBasicBodyAnchor.ToPathData();
                    pathHalfBotBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodyAnchor);

                    //Draw bending part with radius 

                    pbRadiusBend.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter);
                    pbRadiusBend.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                    pbRadiusBend.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax);
                    pbRadiusBend.AddCurveTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter,
                        X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter) / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter);

                    pathRadiusBend.PathData = pbRadiusBend.ToPathData();
                    pathRadiusBend.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBend.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBend.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBend);

                    if (anchor.BendLength <= bendLengthMax)
                    {
                        //Draw bending part without radius

                        rectBendAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - (anchor.BendLength - anchor.Diameter),
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter,
                            anchor.BendLength - (anchor.BendRadius + anchor.Diameter),
                            anchor.Diameter,
                            Color.Transparent,
                            Color.Black,
                            1.5f,
                            SvgLengthUnits.Pixels);

                        svgElements.Add(rectBendAnchor);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + anchor.ThreadLength + lengthMax,
                              X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + anchor.ThreadLength + lengthMax + (outPartHorSize + 5),
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter,
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.ThreadLength + lengthMax + (outPartHorSize + 5),
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                              Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread
                    }
                    else
                    {
                        //Draw bending part without radius 

                        //Make right half bending part without radius

                        var pbHalfRightBendPartAnchor = new SvgPathBuilder();
                        var pathHalfRightBendPartAnchor = new SvgPathElement();

                        pbHalfRightBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                        pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius);
                        pbHalfRightBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + anchor.ThreadLength);
                        pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap);

                        pathHalfRightBendPartAnchor.PathData = pbHalfRightBendPartAnchor.ToPathData();
                        pathHalfRightBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathHalfRightBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                        pathHalfRightBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                        svgElements.Add(pathHalfRightBendPartAnchor);

                        //Make left half bending part without radius

                        var pbHalfLeftBendPartAnchor = new SvgPathBuilder();
                        var pathHalfLeftBendPartAnchor = new SvgPathElement();

                        pbHalfLeftBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                        pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax);
                        pbHalfLeftBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + anchor.ThreadLength);
                        pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2);

                        pathHalfLeftBendPartAnchor.PathData = pbHalfLeftBendPartAnchor.ToPathData();
                        pathHalfLeftBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathHalfLeftBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                        pathHalfLeftBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                        svgElements.Add(pathHalfLeftBendPartAnchor);

                        // Make gap Right Line

                        var pbgapRight = new SvgPathBuilder();
                        var pathgapRight = new SvgPathElement();

                        pbgapRight.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                               Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                        pbgapRight.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap - 5,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                            Y_InitCoord + lengthMax + anchor.ThreadLength,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                            Y_InitCoord + lengthMax + anchor.ThreadLength);

                        pathgapRight.PathData = pbgapRight.ToPathData();
                        pathgapRight.Fill = new SvgPaint(Color.Transparent);
                        pathgapRight.Stroke = new SvgPaint(Color.Black);
                        pathgapRight.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapRight);

                        // Make gap Left Line

                        var pbgapLeft = new SvgPathBuilder();
                        var pathgapLeft = new SvgPathElement();

                        pbgapLeft.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                        pbgapLeft.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 - 5,
                            Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + anchor.ThreadLength,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + anchor.ThreadLength);

                        pathgapLeft.PathData = pbgapLeft.ToPathData();
                        pathgapLeft.Fill = new SvgPaint(Color.Transparent);
                        pathgapLeft.Stroke = new SvgPaint(Color.Black);
                        pathgapLeft.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapLeft);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + lengthMax + anchor.ThreadLength,
                              X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + lengthMax + anchor.ThreadLength + (outPartHorSize + 5),
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + lengthMax + anchor.ThreadLength - (anchor.BendRadius + anchor.Diameter),
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + lengthMax + anchor.ThreadLength + (outPartHorSize + 5),
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                              Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread
                    }

                    //Size of radius

                    var lineInclinSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                         Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                         X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                         Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineInclinSizeRadius);

                    var lineHorSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                      Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                      Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeRadius);

                    var lineSerifSizeRadius = GetSerifRad(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                      Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                      Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifSizeRadius);

                    svgElements.Add(GetSvgTextElement($"R{anchor.BendRadius}",
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                        0,
                        SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor

                    //Make bottom part anchor's length

                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                    Y_InitCoord + anchor.ThreadLength + lengthMax,
                                    X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                    Y_InitCoord + anchor.ThreadLength + lengthMax,
                                    Color.Black,
                                    0.5f,
                                    SvgLengthUnits.Pixels);

                    lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.ThreadLength + lengthMax,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.ThreadLength + lengthMax,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.ThreadLength + lengthMax,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                    svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                          Y_InitCoord + (anchor.ThreadLength + lengthMax) / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor
              
             

                svgElements.Add(lineHorBotSizeLengthOfAnchor);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);
            }

            // GetDescriptionAnchor(anchor, paramsCanvas, svgElements); 

            for (int i = 0; i < svgElements.Count; i++)
                svgDoc.RootSvg.Children.Insert(i, svgElements[i]);

            SvgViewBox view = new();
            view.MinX = 0;
            view.MinY = 0;
            view.Width = 900;
            view.Height = 1100;

            svgDoc.RootSvg.ViewBox = view;

            StringBuilder stringBuilder = new();
            svgDoc.Save(stringBuilder);
            string xml = stringBuilder.ToString();
            string svgElem = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            anchor.SvgElement = svgElem;
        }

        public void GetSvgBendDoubleAnchor(Anchor anchor)
        {
            float ThreadStep = (float)anchor.ThreadStep; // parse to float double threadstep
            int lengthMax = 800; // max length of anchor
            int bendLengthMax = 300; // max length of anchor's bend

            if (anchor.BendLength <= bendLengthMax)
                X_InitCoord += anchor.BendLength; // X origin
            else
                X_InitCoord += bendLengthMax; // X origin

            int gap = 20; // gap in out of max length  of anchor
            int outPartHorSize = 30; // length output part of horizontal size
            int outPartRadSize = 45; // length of shelf of radius size

            var svgDoc = new GcSvgDocument();
            svgDoc.RootSvg.Width = new SvgLength(500, SvgLengthUnits.Pixels);
            svgDoc.RootSvg.Height = new SvgLength(500, SvgLengthUnits.Pixels);

            List<SvgElement> svgElements = new(); // Make list to fill with objects SvgRectElement

            //Draw part with thread

            var rectThreadBodyAnchor = GetSvgRectElement(X_InitCoord,
                Y_InitCoord,
                anchor.ThreadDiameter,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1.5f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadBodyAnchor);

            var rectThreadStepBodyAnchor = GetSvgRectElement(X_InitCoord + ThreadStep / 2,
                Y_InitCoord,
                anchor.ThreadDiameter - ThreadStep,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadStepBodyAnchor);

            //Draw second part with thread

            SvgRectElement rectThreadBodyAnchorSecond;
            SvgRectElement rectThreadStepBodyAnchorSecond;

            if (anchor.BendLength <= bendLengthMax)
            {
                rectThreadBodyAnchorSecond = GetSvgRectElement(X_InitCoord - anchor.BendLength + anchor.Diameter,
                Y_InitCoord,
                anchor.ThreadDiameter,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1.5f,
                SvgLengthUnits.Pixels);

                svgElements.Add(rectThreadBodyAnchorSecond);

                rectThreadStepBodyAnchorSecond = GetSvgRectElement(X_InitCoord + ThreadStep / 2 - anchor.BendLength + anchor.Diameter,
                Y_InitCoord,
                anchor.ThreadDiameter - ThreadStep,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1f,
                SvgLengthUnits.Pixels);

                svgElements.Add(rectThreadStepBodyAnchorSecond);
            }
            else
            {
                rectThreadBodyAnchorSecond = GetSvgRectElement(X_InitCoord - bendLengthMax,
                                Y_InitCoord,
                                anchor.ThreadDiameter,
                                anchor.ThreadLength,
                                Color.Transparent,
                                Color.Black,
                                1.5f,
                                SvgLengthUnits.Pixels);

                svgElements.Add(rectThreadBodyAnchorSecond);

                rectThreadStepBodyAnchorSecond = GetSvgRectElement(X_InitCoord + ThreadStep / 2 - bendLengthMax,
                Y_InitCoord,
                anchor.ThreadDiameter - ThreadStep,
                anchor.ThreadLength,
                Color.Transparent,
                Color.Black,
                1f,
                SvgLengthUnits.Pixels);

                svgElements.Add(rectThreadStepBodyAnchorSecond);
            }

            // Draw sizes of part with thread

            // Size of thread's diametr

            var lineVertLeftSizeDiamThread = GetSvgLineElement(X_InitCoord,
                Y_InitCoord,
                X_InitCoord,
                Y_InitCoord - outPartHorSize,
                Color.Black,
                0.5f,
                SvgLengthUnits.Pixels);

            svgElements.Add(lineVertLeftSizeDiamThread);

            var lineVertRightSizeDiamThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                  Y_InitCoord,
                  X_InitCoord + anchor.ThreadDiameter,
                  Y_InitCoord - outPartHorSize,
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

            svgElements.Add(lineVertRightSizeDiamThread);

            var lineHorSizeDiamThread = GetSvgLineElement(X_InitCoord,
                       Y_InitCoord - (outPartHorSize - 5),
                       X_InitCoord + anchor.ThreadDiameter + 50,
                       Y_InitCoord - (outPartHorSize - 5),
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamThread);

            var lineSerifLeftSizeDiamThread = GetSerif(X_InitCoord,
                        Y_InitCoord - (outPartHorSize - 5),
                        X_InitCoord,
                        Y_InitCoord - (outPartHorSize - 5),
                        Color.Black,
                        0.5f,
                        SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamThread);

            var lineSerifRightSizeDiamThread = GetSerif(X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord - (outPartHorSize - 5),
                           X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord - (outPartHorSize - 5),
                           Color.Black,
                           0.5f,
                           SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamThread);


            svgElements.Add(GetSvgTextElement($"М{anchor.ThreadDiameter}x{ThreadStep}",
                            X_InitCoord + anchor.ThreadDiameter,
                            Y_InitCoord - (outPartHorSize - 5),
                            0,
                            SvgLengthUnits.Pixels));    // Make text of size's value diametr of thread

            // Size of thread's length

            var lineHorTopSizeLengthThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                      Y_InitCoord,
                      X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                      Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineHorTopSizeLengthThread);

            var lineHorBotSizeLengthThread = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                           Y_InitCoord + anchor.ThreadLength,
                           X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                           Y_InitCoord + anchor.ThreadLength,
                           Color.Black,
                           0.5f,
                           SvgLengthUnits.Pixels);

            svgElements.Add(lineHorBotSizeLengthThread);

            var lineVerSizeDiamThread = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                 Y_InitCoord,
                                 X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                 Y_InitCoord + anchor.ThreadLength,
                                 Color.Black,
                                 0.5f,
                                 SvgLengthUnits.Pixels);

            svgElements.Add(lineVerSizeDiamThread);

            var lineSerifTopSizeDiamThread = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                             Y_InitCoord,
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                             Y_InitCoord,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifTopSizeDiamThread);

            var lineSerifBotSizeDiamThread = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                  Y_InitCoord + anchor.ThreadLength,
                                  X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                  Y_InitCoord + anchor.ThreadLength,
                                  Color.Black,
                                  0.5f,
                                  SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifBotSizeDiamThread);

            svgElements.Add(GetSvgTextElement($"{anchor.ThreadLength}",
                X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                Y_InitCoord + anchor.ThreadLength / 2 + 10,
                -90,
                SvgLengthUnits.Pixels));    // Make text of size's value length of thread

            // Size of anchors's diametr

            var lineHorSizeDiamAnchor = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                       X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 40,
                       Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamAnchor);

            var lineSerifLeftSizeDiamAnchor = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamAnchor);

            var lineSerifRightSizeDiamAnchor = GetSerif(X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                     X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamAnchor);

            svgElements.Add(GetSvgTextElement($"d{anchor.Diameter}",
                X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 5,
                Y_InitCoord + anchor.ThreadLength + outPartHorSize,
                0,
                SvgLengthUnits.Pixels));    // Make text of size's value diametr of anchor

            //Make objects basic part without thread and bend 

            SvgRectElement rectBasicBodyAnchor;
            SvgRectElement rectBasicBodyAnchorSecond;

            //Make objects of sizes anchor's length

            SvgLineElement lineHorTopSizeLengthOfAnchor;
            SvgLineElement lineHorBotSizeLengthOfAnchor;
            SvgLineElement lineVertSizeLengthOfAnchor;

            //Make object bending part without radius

            SvgRectElement rectBendAnchor;

            //Make objects bending part with radius

            var pbRadiusBend = new SvgPathBuilder();
            var pathRadiusBend = new SvgPathElement();
            var pbRadiusBendSecond = new SvgPathBuilder();
            var pathRadiusBendSecond = new SvgPathElement();

            if (anchor.Length <= lengthMax)
            {

                //Draw basic part without thread and bend 

                rectBasicBodyAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + anchor.ThreadLength,
                    anchor.Diameter,
                    anchor.Length - (anchor.ThreadLength + anchor.BendRadius + anchor.Diameter),
                    Color.Transparent,
                    Color.Black,
                    1.5f,
                    SvgLengthUnits.Pixels);

                if (anchor.BendLength <= bendLengthMax)
                {
                    //Draw second basic part without thread and bend 

                    rectBasicBodyAnchorSecond = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength,
                        anchor.Diameter,
                        anchor.Length - (anchor.ThreadLength + anchor.BendRadius + anchor.Diameter),
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);

                    //Draw bending part without radius

                    rectBendAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius, 
                        Y_InitCoord + anchor.Length - anchor.Diameter,
                        anchor.BendLength - (anchor.BendRadius + anchor.Diameter) * 2,
                        anchor.Diameter,
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);

                    svgElements.Add(rectBendAnchor);

                    //Size of bending part

                    var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                          X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPart);

                    var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPart);

                    var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPart);

                    var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPart);

                    var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPart);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                          Y_InitCoord + anchor.Length + outPartHorSize,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of thread

                    //Draw second bending part with radius 

                    pbRadiusBendSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + anchor.Length - anchor.Diameter);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength,
                        Y_InitCoord + anchor.Length - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength,
                        Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength,
                        Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius);
                    pbRadiusBendSecond.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength - anchor.Diameter);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength - anchor.Diameter,
                        Y_InitCoord + anchor.Length,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + anchor.Length,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + anchor.Length);


                    pathRadiusBendSecond.PathData = pbRadiusBendSecond.ToPathData();
                    pathRadiusBendSecond.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBendSecond.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBendSecond.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBendSecond);
                }
                else
                {
                    //Draw second basic part without thread and bend 

                    rectBasicBodyAnchorSecond = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength,
                        anchor.Diameter,
                        anchor.Length - (anchor.ThreadLength + anchor.BendRadius + anchor.Diameter),
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);

                    //Draw bending part without radius 

                    //Make right half bending part without radius

                    var pbHalfRightBendPartAnchor = new SvgPathBuilder();
                    var pathHalfRightBendPartAnchor = new SvgPathElement();

                    pbHalfRightBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                        Y_InitCoord + anchor.Length - anchor.Diameter);
                    pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius);
                    pbHalfRightBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.Length);
                    pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap);

                    pathHalfRightBendPartAnchor.PathData = pbHalfRightBendPartAnchor.ToPathData();
                    pathHalfRightBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfRightBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfRightBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfRightBendPartAnchor);

                    //Make left half bending part without radius

                    var pbHalfLeftBendPartAnchor = new SvgPathBuilder();
                    var pathHalfLeftBendPartAnchor = new SvgPathElement();

                    pbHalfLeftBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + anchor.Length - anchor.Diameter);
                    pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.BendRadius + anchor.Diameter);
                    pbHalfLeftBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.Length);
                    pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2);

                    pathHalfLeftBendPartAnchor.PathData = pbHalfLeftBendPartAnchor.ToPathData();
                    pathHalfLeftBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfLeftBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfLeftBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfLeftBendPartAnchor);

                    // Make gap Right Line

                    var pbgapRight = new SvgPathBuilder();
                    var pathgapRight = new SvgPathElement();

                    pbgapRight.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                           Y_InitCoord + anchor.Length - anchor.Diameter);
                    pbgapRight.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap - 5,
                        Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                        Y_InitCoord + anchor.Length,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                        Y_InitCoord + anchor.Length);

                    pathgapRight.PathData = pbgapRight.ToPathData();
                    pathgapRight.Fill = new SvgPaint(Color.Transparent);
                    pathgapRight.Stroke = new SvgPaint(Color.Black);
                    pathgapRight.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapRight);

                    // Make gap Left Line

                    var pbgapLeft = new SvgPathBuilder();
                    var pathgapLeft = new SvgPathElement();

                    pbgapLeft.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + anchor.Length - anchor.Diameter);
                    pbgapLeft.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 - 5,
                        Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + anchor.Length,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + anchor.Length);

                    pathgapLeft.PathData = pbgapLeft.ToPathData();
                    pathgapLeft.Fill = new SvgPaint(Color.Transparent);
                    pathgapLeft.Stroke = new SvgPaint(Color.Black);
                    pathgapLeft.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapLeft);

                    //Size of bending part

                    var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + anchor.Length - anchor.BendRadius - anchor.Diameter,
                          X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPart);

                    var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPart);

                    var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPart);

                    var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPart);

                    var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPart);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                          Y_InitCoord + anchor.Length + outPartHorSize,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of thread

                    //Draw second bending part with radius 

                    pbRadiusBendSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter - bendLengthMax + anchor.BendRadius,
                        Y_InitCoord + anchor.Length - anchor.Diameter);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter - bendLengthMax,
                        Y_InitCoord + anchor.Length - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter - bendLengthMax,
                        Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter - bendLengthMax,
                        Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius);
                    pbRadiusBendSecond.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + - bendLengthMax);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                        Y_InitCoord + anchor.Length,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter - bendLengthMax + anchor.BendRadius,
                        Y_InitCoord + anchor.Length,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter - bendLengthMax + anchor.BendRadius,
                        Y_InitCoord + anchor.Length);


                    pathRadiusBendSecond.PathData = pbRadiusBendSecond.ToPathData();
                    pathRadiusBendSecond.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBendSecond.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBendSecond.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBendSecond);
                }

                svgElements.Add(rectBasicBodyAnchorSecond);

                //Draw bending part with radius 

                pbRadiusBend.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter));
                pbRadiusBend.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + anchor.Length - anchor.Diameter,
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                    Y_InitCoord + anchor.Length - anchor.Diameter,
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                    Y_InitCoord + anchor.Length - anchor.Diameter);
                pbRadiusBend.AddVerticalLineTo(false, Y_InitCoord + anchor.Length);
                pbRadiusBend.AddCurveTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                    Y_InitCoord + anchor.Length,
                    X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                    Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                    X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter) / 2,
                    Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter));


                pathRadiusBend.PathData = pbRadiusBend.ToPathData();
                pathRadiusBend.Fill = new SvgPaint(Color.Transparent);
                pathRadiusBend.Stroke = new SvgPaint(Color.Black);
                pathRadiusBend.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathRadiusBend);

                //Size of radius

                var lineInclinSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                     Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                     X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                     Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

                svgElements.Add(lineInclinSizeRadius);

                var lineHorSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                  Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                  X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                  Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

                svgElements.Add(lineHorSizeRadius);

                var lineSerifSizeRadius = GetSerifRad(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                        Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                        Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifSizeRadius);

                svgElements.Add(GetSvgTextElement($"R{anchor.BendRadius}",
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                    Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius,
                    0,
                    SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor



                svgElements.Add(rectBasicBodyAnchor);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);


                lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                 Y_InitCoord + anchor.Length,
                                 X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                 Y_InitCoord + anchor.Length,
                                 Color.Black,
                                 0.5f,
                                 SvgLengthUnits.Pixels);



                svgElements.Add(lineHorBotSizeLengthOfAnchor);


                lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);

                var lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                          Y_InitCoord + anchor.Length / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor
            }
            else
            {
                //Draw basic part without thread and bend 

                //Make top half basic part without thread and bend

                var pbHalfTopBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfTopBasicBodyAnchor = new SvgPathElement();

                pbHalfTopBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength);
                pbHalfTopBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                pathHalfTopBasicBodyAnchor.PathData = pbHalfTopBasicBodyAnchor.ToPathData();
                pathHalfTopBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                pathHalfTopBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                pathHalfTopBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathHalfTopBasicBodyAnchor);

                // Make gap Top Line

                var pbgapTop = new SvgPathBuilder();
                var pathgapTop = new SvgPathElement();

                pbgapTop.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                pbgapTop.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap) - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap),
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                pathgapTop.PathData = pbgapTop.ToPathData();
                pathgapTop.Fill = new SvgPaint(Color.Transparent);
                pathgapTop.Stroke = new SvgPaint(Color.Black);
                pathgapTop.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapTop);

                SvgLineElement lineSerifBotSizeLengthOfAnchor;

                // Make gap Bot Line

                var pbgapBot = new SvgPathBuilder();
                var pathgapBot = new SvgPathElement();

                pbgapBot.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                pbgapBot.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2 - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                pathgapBot.PathData = pbgapBot.ToPathData();
                pathgapBot.Fill = new SvgPaint(Color.Transparent);
                pathgapBot.Stroke = new SvgPaint(Color.Black);
                pathgapBot.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapBot);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);


                var pbHalfBotBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfBotBasicBodyAnchor = new SvgPathElement();


                //Make bottom half basic part without thread and bend

                pbHalfBotBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                                Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter);
                pbHalfBotBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                pathHalfBotBasicBodyAnchor.PathData = pbHalfBotBasicBodyAnchor.ToPathData();
                pathHalfBotBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                pathHalfBotBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                pathHalfBotBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathHalfBotBasicBodyAnchor);

                //Draw bending part with radius 

                pbRadiusBend.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter);
                pbRadiusBend.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter,
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                    Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter,
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                    Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                pbRadiusBend.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax);
                pbRadiusBend.AddCurveTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                    Y_InitCoord + lengthMax + anchor.ThreadLength,
                    X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                    Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter,
                    X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter) / 2,
                    Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter);

                pathRadiusBend.PathData = pbRadiusBend.ToPathData();
                pathRadiusBend.Fill = new SvgPaint(Color.Transparent);
                pathRadiusBend.Stroke = new SvgPaint(Color.Black);
                pathRadiusBend.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathRadiusBend);

                //Make obj top half second basic part without thread and bend

                var pbHalfTopBasicBodySecondAnchor = new SvgPathBuilder();
                var pathHalfTopBasicBodySecondAnchor = new SvgPathElement();

                // Make obj gap Top Line of second basic part without thread and bend

                var pbgapTopSecond = new SvgPathBuilder();
                var pathgapTopSecond = new SvgPathElement();

                // Make obj gap Bot Line of second basic part without thread and bend

                var pbgapBotSecond = new SvgPathBuilder();
                var pathgapBotSecond = new SvgPathElement();

                //Make obj bot half second basic part without thread and bend

                var pbHalfBotBasicBodySecondAnchor = new SvgPathBuilder();
                var pathHalfBotBasicBodySecondAnchor = new SvgPathElement();

                if (anchor.BendLength <= bendLengthMax)
                {
                    //Draw second basic part without thread and bend 

                    //Make bottom half basic part without thread and bend

                    pbHalfBotBasicBodySecondAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter);
                    pbHalfBotBasicBodySecondAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + 2 * anchor.Diameter);
                    pbHalfBotBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodySecondAnchor.PathData = pbHalfBotBasicBodySecondAnchor.ToPathData();
                    pathHalfBotBasicBodySecondAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodySecondAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodySecondAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodySecondAnchor);

                    // Make gap Bot Line

                    pbgapBotSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                           Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                    pbgapBotSecond.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + lengthMax / 2 - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + lengthMax / 2,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                    pathgapBotSecond.PathData = pbgapBotSecond.ToPathData();
                    pathgapBotSecond.Fill = new SvgPaint(Color.Transparent);
                    pathgapBotSecond.Stroke = new SvgPaint(Color.Black);
                    pathgapBotSecond.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapBotSecond);

                    //Make top half basic part without thread and bend

                    pbHalfTopBasicBodySecondAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                    pbHalfTopBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength);
                    pbHalfTopBasicBodySecondAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + 2 * anchor.Diameter);
                    pbHalfTopBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                    pathHalfTopBasicBodySecondAnchor.PathData = pbHalfTopBasicBodySecondAnchor.ToPathData();
                    pathHalfTopBasicBodySecondAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfTopBasicBodySecondAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfTopBasicBodySecondAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfTopBasicBodySecondAnchor);

                    // Make gap Top Line

                    pbgapTopSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                           Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                    pbgapTopSecond.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap) - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap),
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                    pathgapTopSecond.PathData = pbgapTopSecond.ToPathData();
                    pathgapTopSecond.Fill = new SvgPaint(Color.Transparent);
                    pathgapTopSecond.Stroke = new SvgPaint(Color.Black);
                    pathgapTopSecond.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapTopSecond);

                    //Draw bending part without radius

                    rectBendAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter,
                        anchor.BendLength - (anchor.BendRadius + anchor.Diameter) * 2,
                        anchor.Diameter,
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);

                    svgElements.Add(rectBendAnchor);

                    //Size of bending part

                    var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter,
                          X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + anchor.ThreadLength + lengthMax + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPart);

                    var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.ThreadLength + lengthMax + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPart);

                    var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPart);

                    var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                               X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPart);

                    var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPart);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                          Y_InitCoord + anchor.ThreadLength + lengthMax + outPartHorSize,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of thread

                    //Draw second bending part with radius 

                    pbRadiusBendSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.Diameter);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius);
                    pbRadiusBendSecond.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength - anchor.Diameter);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength - anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + lengthMax,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + anchor.ThreadLength + lengthMax,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + anchor.ThreadLength + lengthMax);


                    pathRadiusBendSecond.PathData = pbRadiusBendSecond.ToPathData();
                    pathRadiusBendSecond.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBendSecond.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBendSecond.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBendSecond);
                }
                else
                {
                    //Draw second basic part without thread and bend 

                    //Make bottom half basic part without thread and bend

                    pbHalfBotBasicBodySecondAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                    Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter);
                    pbHalfBotBasicBodySecondAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter);
                    pbHalfBotBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodySecondAnchor.PathData = pbHalfBotBasicBodySecondAnchor.ToPathData();
                    pathHalfBotBasicBodySecondAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodySecondAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodySecondAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodySecondAnchor);

                    // Make gap Bot Line

                    pbgapBotSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                           Y_InitCoord + anchor.ThreadLength + lengthMax / 2);
                    pbgapBotSecond.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength + lengthMax / 2 - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength + lengthMax / 2,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength + lengthMax / 2);

                    pathgapBotSecond.PathData = pbgapBotSecond.ToPathData();
                    pathgapBotSecond.Fill = new SvgPaint(Color.Transparent);
                    pathgapBotSecond.Stroke = new SvgPaint(Color.Black);
                    pathgapBotSecond.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapBotSecond);

                    //Make top half basic part without thread and bend

                    pbHalfTopBasicBodySecondAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                    pbHalfTopBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength);
                    pbHalfTopBasicBodySecondAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter);
                    pbHalfTopBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                    pathHalfTopBasicBodySecondAnchor.PathData = pbHalfTopBasicBodySecondAnchor.ToPathData();
                    pathHalfTopBasicBodySecondAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfTopBasicBodySecondAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfTopBasicBodySecondAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfTopBasicBodySecondAnchor);

                    // Make gap Top Line

                    pbgapTopSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                           Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));
                    pbgapTopSecond.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap) - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap),
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength + (lengthMax / 2 - gap));

                    pathgapTopSecond.PathData = pbgapTopSecond.ToPathData();
                    pathgapTopSecond.Fill = new SvgPaint(Color.Transparent);
                    pathgapTopSecond.Stroke = new SvgPaint(Color.Black);
                    pathgapTopSecond.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapTopSecond);

                    //Draw bending part without radius 

                    //Make right half bending part without radius

                    var pbHalfRightBendPartAnchor = new SvgPathBuilder();
                    var pathHalfRightBendPartAnchor = new SvgPathElement();

                    pbHalfRightBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                    pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius);
                    pbHalfRightBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + anchor.ThreadLength);
                    pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap);

                    pathHalfRightBendPartAnchor.PathData = pbHalfRightBendPartAnchor.ToPathData();
                    pathHalfRightBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfRightBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfRightBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfRightBendPartAnchor);

                    //Make left half bending part without radius

                    var pbHalfLeftBendPartAnchor = new SvgPathBuilder();
                    var pathHalfLeftBendPartAnchor = new SvgPathElement();

                    pbHalfLeftBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                    pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.BendRadius + anchor.Diameter);
                    pbHalfLeftBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + anchor.ThreadLength);
                    pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2);

                    pathHalfLeftBendPartAnchor.PathData = pbHalfLeftBendPartAnchor.ToPathData();
                    pathHalfLeftBendPartAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfLeftBendPartAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfLeftBendPartAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfLeftBendPartAnchor);

                    // Make gap Right Line

                    var pbgapRight = new SvgPathBuilder();
                    var pathgapRight = new SvgPathElement();

                    pbgapRight.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                           Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                    pbgapRight.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap - 5,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter / 2,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                        Y_InitCoord + lengthMax + anchor.ThreadLength,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                        Y_InitCoord + lengthMax + anchor.ThreadLength);

                    pathgapRight.PathData = pbgapRight.ToPathData();
                    pathgapRight.Fill = new SvgPaint(Color.Transparent);
                    pathgapRight.Stroke = new SvgPaint(Color.Black);
                    pathgapRight.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapRight);

                    // Make gap Left Line

                    var pbgapLeft = new SvgPathBuilder();
                    var pathgapLeft = new SvgPathElement();

                    pbgapLeft.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter);
                    pbgapLeft.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 - 5,
                        Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.Diameter / 2,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + lengthMax + anchor.ThreadLength);

                    pathgapLeft.PathData = pbgapLeft.ToPathData();
                    pathgapLeft.Fill = new SvgPaint(Color.Transparent);
                    pathgapLeft.Stroke = new SvgPaint(Color.Black);
                    pathgapLeft.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapLeft);

                    //Size of bending part

                    var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + lengthMax + anchor.ThreadLength - anchor.BendRadius - anchor.Diameter,
                          X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + lengthMax + anchor.ThreadLength + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPart);

                    var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + lengthMax + anchor.ThreadLength - (anchor.BendRadius + anchor.Diameter),
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + lengthMax + anchor.ThreadLength + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPart);

                    var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPart);

                    var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                               X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPart);

                    var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPart);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                          Y_InitCoord + lengthMax + anchor.ThreadLength + outPartHorSize,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of thread

                    //Draw second bending part with radius 

                    pbRadiusBendSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.BendRadius + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.Diameter);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius);
                    pbRadiusBendSecond.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                        Y_InitCoord + anchor.ThreadLength + lengthMax,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.BendRadius + anchor.Diameter,
                        Y_InitCoord + anchor.ThreadLength + lengthMax);


                    pathRadiusBendSecond.PathData = pbRadiusBendSecond.ToPathData();
                    pathRadiusBendSecond.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBendSecond.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBendSecond.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBendSecond);
                }

                //Size of radius

                var lineInclinSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                     Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                     X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                     Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

                svgElements.Add(lineInclinSizeRadius);

                var lineHorSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                  Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                  X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                  Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

                svgElements.Add(lineHorSizeRadius);

                var lineSerifSizeRadius = GetSerifRad(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                  Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                  X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                  Y_InitCoord + anchor.ThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifSizeRadius);

                svgElements.Add(GetSvgTextElement($"R{anchor.BendRadius}",
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                    Y_InitCoord + anchor.ThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                    0,
                    SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor

                //Make bottom part anchor's length

                lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                Y_InitCoord + anchor.ThreadLength + lengthMax,
                                X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                Y_InitCoord + anchor.ThreadLength + lengthMax,
                                Color.Black,
                                0.5f,
                                SvgLengthUnits.Pixels);

                lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord + anchor.ThreadLength + lengthMax,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord + anchor.ThreadLength + lengthMax,
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

                lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord + anchor.ThreadLength + lengthMax,
                        Color.Black,
                        0.5f,
                        SvgLengthUnits.Pixels);

                svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                      X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                      Y_InitCoord + (anchor.ThreadLength + lengthMax) / 2 + 10,
                      -90,
                      SvgLengthUnits.Pixels));    // Make text of size's value length of anchor



                svgElements.Add(lineHorBotSizeLengthOfAnchor);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);
            }

            // GetDescriptionAnchor(anchor, paramsCanvas, svgElements); 

            for (int i = 0; i < svgElements.Count; i++)
                svgDoc.RootSvg.Children.Insert(i, svgElements[i]);

            SvgViewBox view = new();
            view.MinX = 0;
            view.MinY = 0;
            view.Width = 900;
            view.Height = 1100;

            svgDoc.RootSvg.ViewBox = view;

            StringBuilder stringBuilder = new();
            svgDoc.Save(stringBuilder);
            string xml = stringBuilder.ToString();
            string svgElem = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            anchor.SvgElement = svgElem;
        }

        //Method for making text
        static SvgTextElement GetSvgTextElement(string content, float x, float y, float angle, SvgLengthUnits units)
        {
            var textSizeThreadLegth = new SvgContentElement
            {
                Content = $"{content}",
                Stroke = new SvgPaint(Color.Black),
                Color = new SvgColor(Color.Black),
            };

            var coordsXSizeThreadLegth = new List<SvgLength>();
            coordsXSizeThreadLegth.Add(new SvgLength(x, units));
            var coordsYSizeThreadLegth = new List<SvgLength>();
            coordsYSizeThreadLegth.Add(new SvgLength(y, units));

            var transforms = new List<SvgTransform>();
            var svgRotateTrans = new SvgRotateTransform()
            {
                Angle = new SvgAngle(angle),
                CenterX = new SvgLength(x),
                CenterY = new SvgLength(y),
            };
            transforms.Add(svgRotateTrans);

            SvgTextElement svgTextElement = new SvgTextElement
            {
                X = coordsXSizeThreadLegth,
                Y = coordsYSizeThreadLegth,
                Color = new SvgColor(Color.Black),
                FontStyle = SvgFontStyle.Normal,
                FontSize = new SvgLength(25, units),
                FontWeight = SvgFontWeight.Bold,
                Transform = transforms,
                TextOrientation = SvgTextOrientation.Mixed
            };

            svgTextElement.Children.Insert(0, textSizeThreadLegth);

            return svgTextElement;
        }

        //Method for making line
        static SvgLineElement GetSvgLineElement(float x1, float y1, float x2, float y2, Color color, float width, SvgLengthUnits units)
        {
            var svgLineElement = new SvgLineElement
            {
                X1 = new SvgLength(x1, units),
                Y1 = new SvgLength(y1, units),
                X2 = new SvgLength(x2, units),
                Y2 = new SvgLength(y2, units),
                Stroke = new SvgPaint(color),
                StrokeWidth = new SvgLength(width, units)
            };

            return svgLineElement;
        }

        //Method for making rectangle
        static SvgRectElement GetSvgRectElement(float x, float y, float width, float height, Color colorOfFill, Color colorOfStroke, float strokeWidth, SvgLengthUnits units)
        {
            var svgRectElement = new SvgRectElement
            {
                X = new SvgLength(x, units),
                Y = new SvgLength(y, units),
                Width = new SvgLength(width, units),
                Height = new SvgLength(height, units),
                Fill = new SvgPaint(colorOfFill),
                Stroke = new SvgPaint(colorOfStroke),
                StrokeWidth = new SvgLength(strokeWidth, units)
            };

            return svgRectElement;
        }

        //Method for making serif
        static SvgLineElement GetSerif(float x1, float y1, float x2, float y2, Color color, float width, SvgLengthUnits units)
        {
            var svgLineElement = new SvgLineElement
            {
                X1 = new SvgLength(x1 - 3, units),
                Y1 = new SvgLength(y1 + 3, units),
                X2 = new SvgLength(x2 + 3, units),
                Y2 = new SvgLength(y2 - 3, units),
                Stroke = new SvgPaint(color),
                StrokeWidth = new SvgLength(width, units)
            };
            return svgLineElement;
        }

        //Method for making serif for radius
        static SvgLineElement GetSerifRad(float x1, float y1, float x2, float y2, Color color, float width, SvgLengthUnits units)
        {
            var svgLineElement = new SvgLineElement
            {
                X1 = new SvgLength(x1 - 2, units),
                Y1 = new SvgLength(y1 + 5, units),
                X2 = new SvgLength(x2 + 2, units),
                Y2 = new SvgLength(y2 - 5, units),
                Stroke = new SvgPaint(color),
                StrokeWidth = new SvgLength(width, units)
            };
            return svgLineElement;
        }
    }
}
