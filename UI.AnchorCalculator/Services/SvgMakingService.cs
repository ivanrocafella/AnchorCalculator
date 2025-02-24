using Core.AnchorCalculator.Entities;
using GrapeCity.Documents.Svg;
using System.Drawing;
using System.Text;

namespace UI.AnchorCalculator.Services
{
    public class SvgMakingService
    {
        public int X_InitCoord = 325; // X origin
        const int Y_InitCoord = 100; // Y origin
        const int viewWidth = 900;
        const int viewHeight = 1200;
        const int Width = 900;
        const int Height = 900;
        const int lengthMax = 700; // max length of anchor
        const int bendLengthMax = 300; // max length of anchor's bend
        float scaledThreadLength;
        float scaledSecondThreadLength;
        float scaledLength;

        public void GetSvgStraightAnchor(Anchor anchor)
        {
            string iconDiameter = anchor.Material.TypeId == 1 ? "Арм" : "⌀";

            if (anchor.BendLength <= bendLengthMax)
                X_InitCoord += anchor.BendLength; // X origin
            else
                X_InitCoord += bendLengthMax; // X origin

            GetScaledLength(anchor.ThreadLength, anchor.ThreadLengthSecond); // scaling threadLength

            int gap = 20; // gap in out of max length  of anchor
            int outPartHorSize = 45; // length output part of horizontal size
            int outPartRadSize = 45; // length of shelf of radius size

            var svgDoc = new GcSvgDocument();
            svgDoc.RootSvg.Width = new SvgLength(Width, SvgLengthUnits.Pixels);
            svgDoc.RootSvg.Height = new SvgLength(Height, SvgLengthUnits.Pixels);

            List<SvgElement> svgElements = new(); // Make list to fill with objects SvgRectElement

            //Draw part with thread

            var rectThreadBodyAnchor = GetSvgRectElement(X_InitCoord,
                Y_InitCoord,
                anchor.ThreadDiameter,
                scaledThreadLength,
                Color.Transparent,
                Color.Black,
                1.5f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadBodyAnchor);

            var rectThreadStepBodyAnchor = GetSvgRectElement(X_InitCoord + anchor.ThreadStep / 2,
                Y_InitCoord,
                anchor.ThreadDiameter - anchor.ThreadStep,
                scaledThreadLength,
                Color.Transparent,
                Color.Black,
                1f,
                SvgLengthUnits.Pixels);

            svgElements.Add(rectThreadStepBodyAnchor);

            if (anchor.ThreadLength > 0)
            {
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
                           X_InitCoord + anchor.ThreadDiameter + 105,
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

                svgElements.Add(GetSvgTextElement($"М{anchor.ThreadDiameter}x{anchor.ThreadStep}",
                                X_InitCoord + anchor.ThreadDiameter + 8,
                                Y_InitCoord - (outPartHorSize - 3),
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
                               Y_InitCoord + scaledThreadLength,
                               X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                               Y_InitCoord + scaledThreadLength,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                svgElements.Add(lineHorBotSizeLengthThread);

                var lineVerSizeDiamThread = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord,
                                     X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord + scaledThreadLength,
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
                                      Y_InitCoord + scaledThreadLength,
                                      X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                      Y_InitCoord + scaledThreadLength,
                                      Color.Black,
                                      0.5f,
                                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeDiamThread);

                svgElements.Add(GetSvgTextElement($"{anchor.ThreadLength}",
                    X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize - 2,
                    Y_InitCoord + scaledThreadLength / 2 + 10,
                    -90,
                    SvgLengthUnits.Pixels));    // Make text of size's value length of thread
            }

            // Size of anchors's diametr

            var lineHorSizeDiamAnchor = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + scaledThreadLength + outPartHorSize,
                       X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 55,
                       Y_InitCoord + scaledThreadLength + outPartHorSize,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamAnchor);

            var lineSerifLeftSizeDiamAnchor = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + scaledThreadLength + outPartHorSize,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + scaledThreadLength + outPartHorSize,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamAnchor);

            var lineSerifRightSizeDiamAnchor = GetSerif(X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + scaledThreadLength + outPartHorSize,
                     X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + scaledThreadLength + outPartHorSize,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamAnchor);

            svgElements.Add(GetSvgTextElement($"{iconDiameter}{anchor.Diameter}",
                X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 5,
                Y_InitCoord + scaledThreadLength + outPartHorSize - 2,
                0,
                SvgLengthUnits.Pixels)); // Make text of size's value diametr of anchor

            //Make object basic part without thread and bend 

            SvgRectElement rectBasicBodyAnchor;

            //Make objects of sizes anchor's length

            SvgLineElement lineHorTopSizeLengthOfAnchor;
            SvgLineElement lineHorBotSizeLengthOfAnchor;
            SvgLineElement lineVertSizeLengthOfAnchor;
            SvgLineElement lineAxialTopHalfOfAnchor;
            SvgLineElement lineAxialBotHalfOfAnchor;

            //Make objects of part with second anchor's thread

            SvgRectElement rectThreadSecondBodyAnchor;
            SvgRectElement rectThreadSecondStepBodyAnchor;

            // Make objects of secont thread's diametr

            SvgLineElement lineHorTopSizeLengthThreadSecond;
            SvgLineElement lineHorBotSizeLengthThreadSecond;
            SvgLineElement lineVerSizeDiamThreadSecond;
            SvgLineElement lineSerifTopSizeDiamThreadSecond;
            SvgLineElement lineSerifBotSizeDiamThreadSecond;

            //Make object bending part without radius

            //SvgRectElement rectBendAnchor;

            if (anchor.Length <= lengthMax)
            {
                if (anchor.ThreadLengthSecond > 0)
                {
                    // Size of second thread's length

                    lineHorTopSizeLengthThreadSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                              Y_InitCoord + anchor.Length - scaledSecondThreadLength,
                              X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                              Y_InitCoord + anchor.Length - scaledSecondThreadLength,
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorTopSizeLengthThreadSecond);

                    lineHorBotSizeLengthThreadSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                                   Y_InitCoord + anchor.Length,
                                   X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                                   Y_InitCoord + anchor.Length,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorBotSizeLengthThreadSecond);

                    lineVerSizeDiamThreadSecond = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                         Y_InitCoord + anchor.Length - scaledSecondThreadLength,
                                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                         Y_InitCoord + anchor.Length,
                                         Color.Black,
                                         0.5f,
                                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVerSizeDiamThreadSecond);

                    lineSerifTopSizeDiamThreadSecond = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord + anchor.Length - scaledSecondThreadLength,
                                     X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord + anchor.Length - scaledSecondThreadLength,
                                     Color.Black,
                                     0.5f,
                                     SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifTopSizeDiamThreadSecond);

                    lineSerifBotSizeDiamThreadSecond = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                          Y_InitCoord + anchor.Length,
                                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                          Y_InitCoord + anchor.Length,
                                          Color.Black,
                                          0.5f,
                                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifBotSizeDiamThreadSecond);

                    svgElements.Add(GetSvgTextElement($"{anchor.ThreadLengthSecond}",
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize - 2,
                        Y_InitCoord + anchor.Length - scaledSecondThreadLength / 2 + 10,
                        -90,
                        SvgLengthUnits.Pixels));    // Make text of size's value length of second thread

                    //Draw part with second thread

                    rectThreadSecondBodyAnchor = GetSvgRectElement(X_InitCoord,
                        Y_InitCoord + (anchor.Length - scaledSecondThreadLength),
                        anchor.ThreadDiameter,
                        scaledSecondThreadLength,
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);

                    svgElements.Add(rectThreadSecondBodyAnchor);

                    rectThreadSecondStepBodyAnchor = GetSvgRectElement(X_InitCoord + anchor.ThreadStep / 2,
                        Y_InitCoord + (anchor.Length - scaledSecondThreadLength),
                        anchor.ThreadDiameter - anchor.ThreadStep,
                        scaledSecondThreadLength,
                        Color.Transparent,
                        Color.Black,
                        1f,
                        SvgLengthUnits.Pixels);

                    svgElements.Add(rectThreadSecondStepBodyAnchor);

                    //Draw part without thread

                    rectBasicBodyAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + scaledThreadLength,
                    anchor.Diameter,
                    anchor.Length - scaledThreadLength - scaledSecondThreadLength,
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
                              X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2,
                              Y_InitCoord + anchor.Length / 2 + 10,
                              -90,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of anchor

                    lineAxialTopHalfOfAnchor = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord - outPartHorSize,
                                X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord + anchor.Length + outPartHorSize,
                                Color.Black,
                                0.15f,
                                SvgLengthUnits.Pixels);

                    svgElements.Add(lineAxialTopHalfOfAnchor); // Make top axial line of anchor
                }
                else
                {
                    rectBasicBodyAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + scaledThreadLength,
                    anchor.Diameter,
                    anchor.Length - scaledThreadLength,
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
                              X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2,
                              Y_InitCoord + anchor.Length / 2 + 10,
                              -90,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of anchor

                    lineAxialTopHalfOfAnchor = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord - outPartHorSize,
                                X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord + anchor.Length + outPartHorSize,
                                Color.Black,
                                0.15f,
                                SvgLengthUnits.Pixels);

                    svgElements.Add(lineAxialTopHalfOfAnchor); // Make top axial line of anchor
                }               
            }
            else
            {
                //Draw basic part without thread and bend 

                //Make top half basic part without thread and bend

                var pbHalfTopBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfTopBasicBodyAnchor = new SvgPathElement();

                if (anchor.ThreadLengthSecond > 0)
                {
                    // Size of second thread's length

                    lineHorTopSizeLengthThreadSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                              Y_InitCoord + scaledThreadLength + lengthMax - scaledSecondThreadLength,
                              X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                              Y_InitCoord + scaledThreadLength + lengthMax - scaledSecondThreadLength,
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorTopSizeLengthThreadSecond);

                    lineHorBotSizeLengthThreadSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter,
                                   Y_InitCoord + scaledThreadLength + lengthMax,
                                   X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                                   Y_InitCoord + scaledThreadLength + lengthMax,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorBotSizeLengthThreadSecond);

                    lineVerSizeDiamThreadSecond = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                         Y_InitCoord + scaledThreadLength + lengthMax - scaledSecondThreadLength,
                                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                         Y_InitCoord + scaledThreadLength + lengthMax,
                                         Color.Black,
                                         0.5f,
                                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVerSizeDiamThreadSecond);

                    lineSerifTopSizeDiamThreadSecond = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord + scaledThreadLength + lengthMax - scaledSecondThreadLength,
                                     X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord + scaledThreadLength + lengthMax - scaledSecondThreadLength,
                                     Color.Black,
                                     0.5f,
                                     SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifTopSizeDiamThreadSecond);

                    lineSerifBotSizeDiamThreadSecond = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                          Y_InitCoord + scaledThreadLength + lengthMax,
                                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                          Y_InitCoord + scaledThreadLength + lengthMax,
                                          Color.Black,
                                          0.5f,
                                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifBotSizeDiamThreadSecond);

                    svgElements.Add(GetSvgTextElement($"{anchor.ThreadLengthSecond}",
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize - 2,
                        Y_InitCoord + lengthMax + scaledThreadLength - scaledSecondThreadLength / 2 + 10,
                        -90,
                        SvgLengthUnits.Pixels));    // Make text of size's value length of second thread

                    //Draw part with second thread

                    rectThreadSecondBodyAnchor = GetSvgRectElement(X_InitCoord,
                        Y_InitCoord + (lengthMax + scaledThreadLength - scaledSecondThreadLength),
                        anchor.ThreadDiameter,
                        scaledSecondThreadLength,
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);

                    svgElements.Add(rectThreadSecondBodyAnchor);

                    rectThreadSecondStepBodyAnchor = GetSvgRectElement(X_InitCoord + anchor.ThreadStep / 2,
                        Y_InitCoord + (lengthMax + scaledThreadLength - scaledSecondThreadLength),
                        anchor.ThreadDiameter - anchor.ThreadStep,
                        scaledSecondThreadLength,
                        Color.Transparent,
                        Color.Black,
                        1f,
                        SvgLengthUnits.Pixels);

                    svgElements.Add(rectThreadSecondStepBodyAnchor);

                    pbHalfTopBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                    pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength);
                    pbHalfTopBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                    pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

                    pathHalfTopBasicBodyAnchor.PathData = pbHalfTopBasicBodyAnchor.ToPathData();
                    pathHalfTopBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfTopBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfTopBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfTopBasicBodyAnchor);

                    // Make gap Top Line

                    var pbgapTop = new SvgPathBuilder();
                    var pathgapTop = new SvgPathElement();

                    pbgapTop.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                           Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                    pbgapTop.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap) - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

                    pathgapTop.PathData = pbgapTop.ToPathData();
                    pathgapTop.Fill = new SvgPaint(Color.Transparent);
                    pathgapTop.Stroke = new SvgPaint(Color.Black);
                    pathgapTop.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapTop);

                    SvgLineElement lineSerifBotSizeLengthOfAnchor;

                    lineAxialTopHalfOfAnchor = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord - outPartHorSize,
                                X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                                Color.Black,
                                0.15f,
                                SvgLengthUnits.Pixels);

                    svgElements.Add(lineAxialTopHalfOfAnchor); // Make top axial line of anchor

                    // Make gap Bot Line

                    var pbgapBot = new SvgPathBuilder();
                    var pathgapBot = new SvgPathElement();

                    pbgapBot.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                           Y_InitCoord + scaledThreadLength + lengthMax / 2);
                    pbgapBot.AddCurveTo(false, X_InitCoord - anchor.ThreadDiameter / 2 + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2 - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2);

                    pathgapBot.PathData = pbgapBot.ToPathData();
                    pathgapBot.Fill = new SvgPaint(Color.Transparent);
                    pathgapBot.Stroke = new SvgPaint(Color.Black);
                    pathgapBot.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapBot);

                    lineAxialBotHalfOfAnchor = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                           Y_InitCoord + scaledThreadLength + lengthMax / 2,
                           X_InitCoord + anchor.ThreadDiameter / 2,
                           Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                           Color.Black,
                           0.15f,
                           SvgLengthUnits.Pixels);

                    svgElements.Add(lineAxialBotHalfOfAnchor); // Make bot axial line of anchor

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
                                    Y_InitCoord + scaledThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax - scaledSecondThreadLength);
                    pbHalfBotBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodyAnchor.PathData = pbHalfBotBasicBodyAnchor.ToPathData();
                    pathHalfBotBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodyAnchor);

                    // Make size anchors length 

                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                  Y_InitCoord + scaledThreadLength + lengthMax,
                                  X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                  Y_InitCoord + scaledThreadLength + lengthMax,
                                  Color.Black,
                                  0.5f,
                                  SvgLengthUnits.Pixels);

                    lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord + scaledThreadLength + lengthMax,
                           X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord + scaledThreadLength + lengthMax,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

                    lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord,
                       X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord + scaledThreadLength + lengthMax,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

                    svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2,
                         Y_InitCoord + (scaledThreadLength + lengthMax) / 2 + 10,
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
                else
                {
                    pbHalfTopBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                    pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength);
                    pbHalfTopBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                    pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

                    pathHalfTopBasicBodyAnchor.PathData = pbHalfTopBasicBodyAnchor.ToPathData();
                    pathHalfTopBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfTopBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfTopBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfTopBasicBodyAnchor);

                    // Make gap Top Line

                    var pbgapTop = new SvgPathBuilder();
                    var pathgapTop = new SvgPathElement();
                    float halfDiam;

                    if (anchor.ThreadLength > 0) 
                        halfDiam = anchor.Diameter - anchor.ThreadDiameter / 2;                    
                    else
                        halfDiam = 0;

                    pbgapTop.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                           Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                    pbgapTop.AddCurveTo(false, X_InitCoord + halfDiam,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap) - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

                    pathgapTop.PathData = pbgapTop.ToPathData();
                    pathgapTop.Fill = new SvgPaint(Color.Transparent);
                    pathgapTop.Stroke = new SvgPaint(Color.Black);
                    pathgapTop.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapTop);

                    SvgLineElement lineSerifBotSizeLengthOfAnchor;

                    lineAxialTopHalfOfAnchor = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord - outPartHorSize,
                                X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                                Color.Black,
                                0.15f,
                                SvgLengthUnits.Pixels);

                    svgElements.Add(lineAxialTopHalfOfAnchor); // Make top axial line of anchor

                    // Make gap Bot Line

                    var pbgapBot = new SvgPathBuilder();
                    var pathgapBot = new SvgPathElement();

                    pbgapBot.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                           Y_InitCoord + scaledThreadLength + lengthMax / 2);
                    pbgapBot.AddCurveTo(false, X_InitCoord + halfDiam,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2 - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2);

                    pathgapBot.PathData = pbgapBot.ToPathData();
                    pathgapBot.Fill = new SvgPaint(Color.Transparent);
                    pathgapBot.Stroke = new SvgPaint(Color.Black);
                    pathgapBot.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapBot);

                    lineAxialBotHalfOfAnchor = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                           Y_InitCoord + scaledThreadLength + lengthMax / 2,
                           X_InitCoord + anchor.ThreadDiameter / 2,
                           Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                           Color.Black,
                           0.15f,
                           SvgLengthUnits.Pixels);

                    svgElements.Add(lineAxialBotHalfOfAnchor); // Make bot axial line of anchor

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
                                    Y_InitCoord + scaledThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax);
                    pbHalfBotBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodyAnchor.PathData = pbHalfBotBasicBodyAnchor.ToPathData();
                    pathHalfBotBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodyAnchor);

                    // Make size anchors length 

                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                  Y_InitCoord + scaledThreadLength + lengthMax,
                                  X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                  Y_InitCoord + scaledThreadLength + lengthMax,
                                  Color.Black,
                                  0.5f,
                                  SvgLengthUnits.Pixels);

                    lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord + scaledThreadLength + lengthMax,
                           X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord + scaledThreadLength + lengthMax,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

                    lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord,
                       X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord + scaledThreadLength + lengthMax,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

                    svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2,
                         Y_InitCoord + (scaledThreadLength + lengthMax) / 2 + 10,
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
            }

          // GetDescriptionAnchor(anchor, paramsCanvas, svgElements); 

            for (int i = 0; i < svgElements.Count; i++)
                svgDoc.RootSvg.Children.Insert(i, svgElements[i]);

            SvgViewBox view = new()
            {
                MinX = 0,
                MinY = 0,
                Width = viewWidth,
                Height = viewHeight
            };

            svgDoc.RootSvg.ViewBox = view;

            StringBuilder stringBuilder = new();
            svgDoc.Save(stringBuilder);
            string xml = stringBuilder.ToString();
            string svgElem = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            anchor.SvgElement = svgElem;
        }

        public void GetSvgBendAnchor(Anchor anchor)
        {
            string iconDiameter = anchor.Material.TypeId == 1 ? "Арм" : "⌀";

            if (anchor.BendLength <= bendLengthMax)
                X_InitCoord += anchor.BendLength; // X origin
            else
                X_InitCoord += bendLengthMax; // X origin

            GetScaledLength(anchor.ThreadLength, anchor.ThreadLengthSecond); // scaling threadLength

            int gap = 20; // gap in out of max length  of anchor
            int outPartHorSize = 45; // length output part of horizontal size
            int outPartRadSize = 45; // length of shelf of radius size

            var svgDoc = new GcSvgDocument();
            svgDoc.RootSvg.Width = new SvgLength(Width, SvgLengthUnits.Pixels);
            svgDoc.RootSvg.Height = new SvgLength(Height, SvgLengthUnits.Pixels);

            List<SvgElement> svgElements = new(); // Make list to fill with objects SvgRectElement

            if (anchor.ThreadLength > 0)
            {

                //Draw part with thread

                var rectThreadBodyAnchor = GetSvgRectElement(X_InitCoord,
                    Y_InitCoord,
                    anchor.ThreadDiameter,
                    scaledThreadLength,
                    Color.Transparent,
                    Color.Black,
                    1.5f,
                    SvgLengthUnits.Pixels);

                svgElements.Add(rectThreadBodyAnchor);

                var rectThreadStepBodyAnchor = GetSvgRectElement(X_InitCoord + anchor.ThreadStep / 2,
                    Y_InitCoord,
                    anchor.ThreadDiameter - anchor.ThreadStep,
                    scaledThreadLength,
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
                           X_InitCoord + anchor.ThreadDiameter + 105,
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


                svgElements.Add(GetSvgTextElement($"М{anchor.ThreadDiameter}x{anchor.ThreadStep}",
                                X_InitCoord + anchor.ThreadDiameter + 8,
                                Y_InitCoord - (outPartHorSize - 3),
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
                               Y_InitCoord + scaledThreadLength,
                               X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                               Y_InitCoord + scaledThreadLength,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                svgElements.Add(lineHorBotSizeLengthThread);

                var lineVerSizeDiamThread = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord,
                                     X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord + scaledThreadLength,
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
                                      Y_InitCoord + scaledThreadLength,
                                      X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                      Y_InitCoord + scaledThreadLength,
                                      Color.Black,
                                      0.5f,
                                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeDiamThread);

                svgElements.Add(GetSvgTextElement($"{anchor.ThreadLength}",
                    X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize - 2,
                    Y_InitCoord + scaledThreadLength / 2 + 10,
                    -90,
                    SvgLengthUnits.Pixels));    // Make text of size's value length of thread
            }

            // Size of anchors's diametr

            var lineHorSizeDiamAnchor = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + scaledThreadLength + outPartHorSize,
                       X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 55,
                       Y_InitCoord + scaledThreadLength + outPartHorSize,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamAnchor);

            var lineSerifLeftSizeDiamAnchor = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + scaledThreadLength + outPartHorSize,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + scaledThreadLength + outPartHorSize,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamAnchor);

            var lineSerifRightSizeDiamAnchor = GetSerif(X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + scaledThreadLength + outPartHorSize,
                     X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + scaledThreadLength + outPartHorSize,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamAnchor);

            svgElements.Add(GetSvgTextElement($"{iconDiameter}{anchor.Diameter}",
                X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 5,
                Y_InitCoord + scaledThreadLength + outPartHorSize - 2,
                0,
                SvgLengthUnits.Pixels));    // Make text of size's value diametr of anchor

            //Make object basic part without thread and bend 

            SvgRectElement rectBasicBodyAnchor;

            //Make objects of sizes anchor's length

            SvgLineElement lineHorTopSizeLengthOfAnchor;
            SvgLineElement lineHorBotSizeLengthOfAnchor;
            SvgLineElement lineVertSizeLengthOfAnchor;
            SvgLineElement lineSerifTopSizeLengthOfAnchor;

            SvgLineElement lineHorTopSizeLengthOfAnchorWithoutRadius;
            SvgLineElement lineHorBotSizeLengthOfAnchorWithoutRadius;
            SvgLineElement lineVertSizeLengthOfAnchorWithoutRadius;

            SvgPathBuilder pbAxialTopHalfOfAnchor = new();
            SvgPathElement pathAxialTopHalfOfAnchor = new();

            SvgPathBuilder pbAxialBotHalfOfAnchor = new();
            SvgPathElement pathAxialBotHalfOfAnchor = new();

            SvgLineElement lineAxialToptHalfOfAnchor;
            SvgLineElement lineAxialBotLeftHalfOfAnchor;

            //Make object bending part without radius

            SvgRectElement rectBendAnchor;

            //Make object bending part with radius

            var pbRadiusBend = new SvgPathBuilder();
            var pathRadiusBend = new SvgPathElement();

            if (anchor.Length <= lengthMax)
            {
              
                    //Draw basic part without thread and bend 

                    rectBasicBodyAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + scaledThreadLength,
                        anchor.Diameter,
                        anchor.Length - (scaledThreadLength + anchor.BendRadius + anchor.Diameter),
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
                              Y_InitCoord + anchor.Length + (outPartHorSize + 5) + outPartHorSize,
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length + (outPartHorSize + 5) + outPartHorSize,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                              Y_InitCoord + anchor.Length + outPartHorSize - 2 + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread


                    //Size of bending part without radius

                    var lineVertLeftSizeBendPartWithouRadius = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + anchor.Length,
                          X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPartWithouRadius);

                    var lineVertRightSizeBendPartWithouRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                         Y_InitCoord + anchor.Length,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                         Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPartWithouRadius);

                    var lineHorSizeBendPartWithouRadius = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPartWithouRadius);

                    var lineSerifLeftSizeBendPartWithouRadius = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPartWithouRadius);

                    var lineSerifRightSizeBendPartWithouRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPartWithouRadius);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength - anchor.BendRadius - anchor.Diameter}",
                          X_InitCoord + (anchor.ThreadDiameter - anchor.BendLength - anchor.BendRadius) / 2 - 10,
                          Y_InitCoord + anchor.Length + outPartHorSize - 2,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's value of bending part without radius

                    pbAxialTopHalfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                            Y_InitCoord - outPartHorSize);
                        pbAxialTopHalfOfAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter));
                        pbAxialTopHalfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                           Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                           X_InitCoord + anchor.ThreadDiameter / 2,
                           Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                           X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                           Y_InitCoord + anchor.Length - anchor.Diameter / 2);
                        pbAxialTopHalfOfAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.BendLength + outPartHorSize));

                        pathAxialTopHalfOfAnchor.PathData = pbAxialTopHalfOfAnchor.ToPathData();
                        pathAxialTopHalfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathAxialTopHalfOfAnchor.Stroke = new SvgPaint(Color.Black);
                        pathAxialTopHalfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                        svgElements.Add(pathAxialTopHalfOfAnchor); // Make top axial line of anchor
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
                              Y_InitCoord + anchor.Length + (outPartHorSize + 5) + outPartHorSize,
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + anchor.Length + (outPartHorSize + 5) + outPartHorSize,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                              Y_InitCoord + anchor.Length + outPartHorSize - 2 + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value length of thread

                    //Size of bending part without radius

                    var lineVertLeftSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + anchor.Length,
                          X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPartWithoutRadius);

                    var lineVertRightSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                         Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter) + anchor.Diameter + anchor.BendRadius,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                         Y_InitCoord + anchor.Length + (outPartHorSize + 5) ,
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPartWithoutRadius);

                    var lineHorSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPartWithoutRadius);

                    var lineSerifLeftSizeBendPartWithoutRadius = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPartWithoutRadius);

                    var lineSerifRightSizeBendPartWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPartWithoutRadius);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength - anchor.BendRadius - anchor.Diameter}",
                          X_InitCoord + ( anchor.ThreadDiameter - bendLengthMax - anchor.BendRadius ) / 2 - 10,
                          Y_InitCoord + anchor.Length + outPartHorSize - 2,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's value of bending part without radius

                    pbAxialTopHalfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                            Y_InitCoord - outPartHorSize);
                        pbAxialTopHalfOfAnchor.AddVerticalLineTo(false, Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter));
                        pbAxialTopHalfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                           Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                           X_InitCoord + anchor.ThreadDiameter / 2,
                           Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                           X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                           Y_InitCoord + anchor.Length - anchor.Diameter / 2);
                        pbAxialTopHalfOfAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap);

                        pathAxialTopHalfOfAnchor.PathData = pbAxialTopHalfOfAnchor.ToPathData();
                        pathAxialTopHalfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathAxialTopHalfOfAnchor.Stroke = new SvgPaint(Color.Black);
                        pathAxialTopHalfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                        svgElements.Add(pathAxialTopHalfOfAnchor); // Make top axial line of anchor

                        lineAxialBotLeftHalfOfAnchor = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                                Y_InitCoord + anchor.Length - anchor.Diameter/2,
                                X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax - outPartHorSize,
                                Y_InitCoord + anchor.Length - anchor.Diameter/2,
                                Color.Black,
                                0.15f,
                                SvgLengthUnits.Pixels);

                        svgElements.Add(lineAxialBotLeftHalfOfAnchor); // Make bot left part of axial line of anchor
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
                        Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius - 2,
                        0,
                        SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor                       

                svgElements.Add(rectBasicBodyAnchor);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);
           
                lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                 Y_InitCoord + anchor.Length,
                                 X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize + outPartRadSize),
                                 Y_InitCoord + anchor.Length,
                                 Color.Black,
                                 0.5f,
                                 SvgLengthUnits.Pixels);
                
                svgElements.Add(lineHorBotSizeLengthOfAnchor);

                lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord + anchor.Length,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);

                var lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord + anchor.Length,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord + anchor.Length,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2 + outPartRadSize,
                          Y_InitCoord + anchor.Length / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor

                // Size of anchors's length without radius

                lineHorTopSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchorWithoutRadius);

                lineHorBotSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter) + anchor.BendRadius + anchor.Diameter,
                                 Y_InitCoord + anchor.Length - anchor.BendRadius - anchor.Diameter,
                                 X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                 Y_InitCoord + anchor.Length - anchor.BendRadius - anchor.Diameter,
                                 Color.Black,
                                 0.5f,
                                 SvgLengthUnits.Pixels);

                svgElements.Add(lineHorBotSizeLengthOfAnchorWithoutRadius);

                lineVertSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length - anchor.BendRadius - anchor.Diameter,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchorWithoutRadius);

                var lineSerifTopSizeLengthOfAnchorWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchorWithoutRadius);

                var lineSerifBotSizeLengthOfAnchorWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length - anchor.BendRadius - anchor.Diameter,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length - anchor.BendRadius - anchor.Diameter,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchorWithoutRadius);

                svgElements.Add(GetSvgTextElement($"{anchor.Length - anchor.BendRadius - anchor.Diameter}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2,
                          Y_InitCoord + (anchor.Length - anchor.BendRadius - anchor.Diameter) / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor without radius
            }
            else
            {
                //Draw basic part without thread and bend 

                //Make top half basic part without thread and bend

                var pbHalfTopBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfTopBasicBodyAnchor = new SvgPathElement();

                pbHalfTopBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength);
                pbHalfTopBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

                pathHalfTopBasicBodyAnchor.PathData = pbHalfTopBasicBodyAnchor.ToPathData();
                pathHalfTopBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                pathHalfTopBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                pathHalfTopBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathHalfTopBasicBodyAnchor);

                // Make gap Top Line

                var pbgapTop = new SvgPathBuilder();
                var pathgapTop = new SvgPathElement();
                float halfDiam;

                if (anchor.ThreadLength > 0)
                    halfDiam = anchor.Diameter - anchor.ThreadDiameter / 2;
                else
                    halfDiam = 0;

                pbgapTop.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                pbgapTop.AddCurveTo(false, X_InitCoord + halfDiam,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap) - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

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
                       Y_InitCoord + scaledThreadLength + lengthMax / 2);
                pbgapBot.AddCurveTo(false, X_InitCoord + halfDiam,
                    Y_InitCoord + scaledThreadLength + lengthMax / 2 - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + scaledThreadLength + lengthMax / 2,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + scaledThreadLength + lengthMax / 2);

                pathgapBot.PathData = pbgapBot.ToPathData();
                pathgapBot.Fill = new SvgPaint(Color.Transparent);
                pathgapBot.Stroke = new SvgPaint(Color.Black);
                pathgapBot.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapBot);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize) + outPartRadSize,
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);

                    lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                    Y_InitCoord + scaledThreadLength + lengthMax,
                                    X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize) + outPartRadSize,
                                    Y_InitCoord + scaledThreadLength + lengthMax,
                                    Color.Black,
                                    0.5f,
                                    SvgLengthUnits.Pixels);


                     lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                             Y_InitCoord,
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                             Y_InitCoord,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord + scaledThreadLength + lengthMax,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord + scaledThreadLength + lengthMax,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartRadSize,
                            Y_InitCoord + scaledThreadLength + lengthMax,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                    svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2 + outPartRadSize,
                          Y_InitCoord + (scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius) / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor

                // Size of anchors's length without radius

                lineHorTopSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchorWithoutRadius);

                lineHorBotSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter) + anchor.BendRadius + anchor.Diameter,
                                Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                                X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                                Color.Black,
                                0.5f,
                                SvgLengthUnits.Pixels);

                svgElements.Add(lineHorBotSizeLengthOfAnchorWithoutRadius);              

                lineVertSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                        Color.Black,
                        0.5f,
                        SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchorWithoutRadius);

                var lineSerifTopSizeLengthOfAnchorWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                     Y_InitCoord,
                     X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                     Y_InitCoord,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchorWithoutRadius);

                var lineSerifBotSizeLengthOfAnchorWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                       X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                       Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                 Color.Black,
                 0.5f,
                 SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchorWithoutRadius);

                svgElements.Add(GetSvgTextElement($"{anchor.Length - anchor.BendRadius - anchor.Diameter}",
                      X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2,
                      Y_InitCoord + (scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius) / 2 + 10,
                      -90,
                      SvgLengthUnits.Pixels));    // Make text of size's value length of anchor withou radius  


                var pbHalfBotBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfBotBasicBodyAnchor = new SvgPathElement();

              
                    //Make bottom half basic part without thread and bend

                    pbHalfBotBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                                    Y_InitCoord + scaledThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter);
                    pbHalfBotBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                    pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodyAnchor.PathData = pbHalfBotBasicBodyAnchor.ToPathData();
                    pathHalfBotBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodyAnchor);

                    //Draw bending part with radius 

                    pbRadiusBend.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.BendRadius - anchor.Diameter);
                    pbRadiusBend.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                    pbRadiusBend.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax);
                    pbRadiusBend.AddCurveTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.BendRadius - anchor.Diameter,
                        X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter) / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.BendRadius - anchor.Diameter);

                    pathRadiusBend.PathData = pbRadiusBend.ToPathData();
                    pathRadiusBend.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBend.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBend.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBend);

                    lineAxialToptHalfOfAnchor = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                                        Y_InitCoord - outPartHorSize,
                                        X_InitCoord + anchor.ThreadDiameter / 2,
                                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                                        Color.Black,
                                        0.15f,
                                        SvgLengthUnits.Pixels);

                    svgElements.Add(lineAxialToptHalfOfAnchor); // Make top part of axial line of anchor

                if (anchor.BendLength <= bendLengthMax)
                    {
                        //Draw bending part without radius

                        rectBendAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - (anchor.BendLength - anchor.Diameter),
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter,
                            anchor.BendLength - (anchor.BendRadius + anchor.Diameter),
                            anchor.Diameter,
                            Color.Transparent,
                            Color.Black,
                            1.5f,
                            SvgLengthUnits.Pixels);

                        svgElements.Add(rectBendAnchor);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + scaledThreadLength + lengthMax,
                              X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                              Y_InitCoord + scaledThreadLength + lengthMax + (outPartHorSize + 5) + outPartHorSize,
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter,
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + scaledThreadLength + lengthMax + (outPartHorSize + 5) + outPartHorSize,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                                   X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                                   Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                              Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize - 2 + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value of bending part

                    //Size of bending part without radius

                    var lineVertLeftSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + scaledThreadLength + lengthMax,
                          X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + scaledThreadLength + lengthMax + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPartWithoutRadius);

                    var lineVertRightSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                         Y_InitCoord + scaledThreadLength + lengthMax,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                         Y_InitCoord + scaledThreadLength + lengthMax + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPartWithoutRadius);

                    var lineHorSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPartWithoutRadius);

                    var lineSerifLeftSizeBendPartWithoutRadius = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPartWithoutRadius);

                    var lineSerifRightSizeBendPartWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPartWithoutRadius);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength - anchor.BendRadius - anchor.Diameter}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength - anchor.BendRadius - anchor.Diameter) / 2 - 10,
                          Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize - 2,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's of bending part without radius   

                    pbAxialBotHalfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord + scaledThreadLength + lengthMax / 2);
                        pbAxialBotHalfOfAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + scaledThreadLength - (anchor.Diameter + anchor.BendRadius));
                        pbAxialBotHalfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                               Y_InitCoord + lengthMax + scaledThreadLength - (anchor.BendRadius + anchor.Diameter),
                               X_InitCoord + anchor.ThreadDiameter / 2,
                               Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                               X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                               Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2);
                        pbAxialBotHalfOfAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.BendLength + outPartHorSize));
                        
                            pathAxialBotHalfOfAnchor.PathData = pbAxialBotHalfOfAnchor.ToPathData();
                        pathAxialBotHalfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                        pathAxialBotHalfOfAnchor.Stroke = new SvgPaint(Color.Black);
                        pathAxialBotHalfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                        svgElements.Add(pathAxialBotHalfOfAnchor); // Make bot axial line of anchor
                    }
                else
                    {
                        //Draw bending part without radius 

                        //Make right half bending part without radius

                        var pbHalfRightBendPartAnchor = new SvgPathBuilder();
                        var pathHalfRightBendPartAnchor = new SvgPathElement();

                        pbHalfRightBendPartAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                        pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius);
                        pbHalfRightBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + scaledThreadLength);
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
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                        pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax);
                        pbHalfLeftBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + scaledThreadLength);
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
                               Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                        pbgapRight.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap - 5,
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                            Y_InitCoord + lengthMax + scaledThreadLength,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                            Y_InitCoord + lengthMax + scaledThreadLength);

                        pathgapRight.PathData = pbgapRight.ToPathData();
                        pathgapRight.Fill = new SvgPaint(Color.Transparent);
                        pathgapRight.Stroke = new SvgPaint(Color.Black);
                        pathgapRight.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapRight);

                        // Make gap Left Line

                        var pbgapLeft = new SvgPathBuilder();
                        var pathgapLeft = new SvgPathElement();

                        pbgapLeft.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                        pbgapLeft.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 - 5,
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + scaledThreadLength,
                            X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                            Y_InitCoord + lengthMax + scaledThreadLength);

                        pathgapLeft.PathData = pbgapLeft.ToPathData();
                        pathgapLeft.Fill = new SvgPaint(Color.Transparent);
                        pathgapLeft.Stroke = new SvgPaint(Color.Black);
                        pathgapLeft.StrokeWidth = new SvgLength(0.5f);

                        svgElements.Add(pathgapLeft);

                        //Size of bending part

                        var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + lengthMax + scaledThreadLength,
                              X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                              Y_InitCoord + lengthMax + scaledThreadLength + (outPartHorSize + 5) + outPartHorSize,
                              Color.Black,
                              0.5f,
                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertLeftSizeBendPart);

                        var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + lengthMax + scaledThreadLength - (anchor.BendRadius + anchor.Diameter),
                             X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                             Y_InitCoord + lengthMax + scaledThreadLength + (outPartHorSize + 5) + outPartHorSize,
                             Color.Black,
                             0.5f,
                             SvgLengthUnits.Pixels);

                        svgElements.Add(lineVertRightSizeBendPart);

                        var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineHorSizeBendPart);

                        var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                                   X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                   Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifLeftSizeBendPart);

                        var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                                   X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                   Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                                   Color.Black,
                                   0.5f,
                                   SvgLengthUnits.Pixels);

                        svgElements.Add(lineSerifRightSizeBendPart);

                        svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                              X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                              Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize - 2 + outPartHorSize,
                              0,
                              SvgLengthUnits.Pixels));    // Make text of size's value bending part

                    //Size of bending part without radius 

                    var lineVertLeftSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + lengthMax + scaledThreadLength,
                          X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + lengthMax + scaledThreadLength + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPartWithoutRadius);

                    var lineVertRightSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                         Y_InitCoord + lengthMax + scaledThreadLength - (anchor.BendRadius + anchor.Diameter) + anchor.Diameter + anchor.BendRadius,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                         Y_InitCoord + lengthMax + scaledThreadLength + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPartWithoutRadius);

                    var lineHorSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPartWithoutRadius);

                    var lineSerifLeftSizeBendPartWithoutRadius = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPartWithoutRadius);

                    var lineSerifRightSizeBendPartWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPartWithoutRadius);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength - anchor.Diameter - anchor.BendRadius}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax - anchor.Diameter - anchor.BendRadius) / 2 - 10,
                          Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize - 2,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's value without bending part

                    pbAxialBotHalfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                                   Y_InitCoord + scaledThreadLength + lengthMax / 2);
                       pbAxialBotHalfOfAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + scaledThreadLength - (anchor.Diameter + anchor.BendRadius));
                       pbAxialBotHalfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                              Y_InitCoord + lengthMax + scaledThreadLength - (anchor.BendRadius + anchor.Diameter),
                              X_InitCoord + anchor.ThreadDiameter / 2,
                              Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                              X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                              Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2);
                       pbAxialBotHalfOfAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap);

                       pathAxialBotHalfOfAnchor.PathData = pbAxialBotHalfOfAnchor.ToPathData();
                       pathAxialBotHalfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                       pathAxialBotHalfOfAnchor.Stroke = new SvgPaint(Color.Black);
                       pathAxialBotHalfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                       svgElements.Add(pathAxialBotHalfOfAnchor); // Make bot axial line of anchor


                       lineAxialBotLeftHalfOfAnchor = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                                              Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                                              X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax - outPartHorSize,
                                              Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                                              Color.Black,
                                              0.15f,
                                              SvgLengthUnits.Pixels);

                        svgElements.Add(lineAxialBotLeftHalfOfAnchor); // Make bot part of axial line of anchor
                    }

                    //Size of radius

                    var lineInclinSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                         Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                         X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                         Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineInclinSizeRadius);

                    var lineHorSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                      Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                      Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeRadius);

                    var lineSerifSizeRadius = GetSerifRad(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                      Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                      Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifSizeRadius);

                    svgElements.Add(GetSvgTextElement($"R{anchor.BendRadius}",
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                        Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter) - 2,
                        0,
                        SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor

                svgElements.Add(lineHorBotSizeLengthOfAnchor);              

                svgElements.Add(lineVertSizeLengthOfAnchor);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);
            }

            // GetDescriptionAnchor(anchor, paramsCanvas, svgElements); 

            for (int i = 0; i < svgElements.Count; i++)
                svgDoc.RootSvg.Children.Insert(i, svgElements[i]);

            SvgViewBox view = new();
            view.MinX = 0;
            view.MinY = 0;
            view.Width = viewWidth;
            view.Height = viewHeight;

            svgDoc.RootSvg.ViewBox = view;

            StringBuilder stringBuilder = new();
            svgDoc.Save(stringBuilder);
            string xml = stringBuilder.ToString();
            string svgElem = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            anchor.SvgElement = svgElem;
        }

        public void GetSvgBendDoubleAnchor(Anchor anchor)
        {
            string iconDiameter = anchor.Material.TypeId == 1 ? "Арм" : "⌀";

            if (anchor.BendLength <= bendLengthMax)
                X_InitCoord += anchor.BendLength; // X origin
            else
                X_InitCoord += bendLengthMax; // X origin

            GetScaledLength(anchor.ThreadLength, anchor.ThreadLengthSecond); // scaling threadLength

            int gap = 20; // gap in out of max length  of anchor
            int outPartHorSize = 45; // length output part of horizontal size
            int outPartRadSize = 45; // length of shelf of radius size

            var svgDoc = new GcSvgDocument();
            svgDoc.RootSvg.Width = new SvgLength(Width, SvgLengthUnits.Pixels);
            svgDoc.RootSvg.Height = new SvgLength(Height, SvgLengthUnits.Pixels);

            List<SvgElement> svgElements = new(); // Make list to fill with objects SvgRectElement

            if (anchor.ThreadLength > 0)
            {
                //Draw part with thread

                var rectThreadBodyAnchor = GetSvgRectElement(X_InitCoord,
                    Y_InitCoord,
                    anchor.ThreadDiameter,
                    scaledThreadLength,
                    Color.Transparent,
                    Color.Black,
                    1.5f,
                    SvgLengthUnits.Pixels);

                svgElements.Add(rectThreadBodyAnchor);

                var rectThreadStepBodyAnchor = GetSvgRectElement(X_InitCoord + anchor.ThreadStep / 2,
                    Y_InitCoord,
                    anchor.ThreadDiameter - anchor.ThreadStep,
                    scaledThreadLength,
                    Color.Transparent,
                    Color.Black,
                    1f,
                    SvgLengthUnits.Pixels);

                svgElements.Add(rectThreadStepBodyAnchor);

                //Draw second part with thread

                SvgRectElement rectThreadBodyAnchorSecond;
                SvgRectElement rectThreadStepBodyAnchorSecond;

                SvgLineElement threadAxialLineFirst;
                SvgLineElement threadAxialLineSecond;

                threadAxialLineFirst = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                                       Y_InitCoord - outPartHorSize,
                                       X_InitCoord + anchor.ThreadDiameter / 2,
                                       Y_InitCoord + scaledThreadLength,
                                       Color.Black,
                                       0.15f,
                                       SvgLengthUnits.Pixels);

                svgElements.Add(threadAxialLineFirst); // Make top part of axial thread's line of anchor

                if (anchor.BendLength <= bendLengthMax)
                {
                    rectThreadBodyAnchorSecond = GetSvgRectElement(X_InitCoord - anchor.BendLength + anchor.Diameter,
                    Y_InitCoord,
                    anchor.ThreadDiameter,
                    scaledThreadLength,
                    Color.Transparent,
                    Color.Black,
                    1.5f,
                    SvgLengthUnits.Pixels);

                    svgElements.Add(rectThreadBodyAnchorSecond);

                    rectThreadStepBodyAnchorSecond = GetSvgRectElement(X_InitCoord + anchor.ThreadStep / 2 - anchor.BendLength + anchor.Diameter,
                    Y_InitCoord,
                    anchor.ThreadDiameter - anchor.ThreadStep,
                    scaledThreadLength,
                    Color.Transparent,
                    Color.Black,
                    1f,
                    SvgLengthUnits.Pixels);

                    svgElements.Add(rectThreadStepBodyAnchorSecond);

                    threadAxialLineSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                                      Y_InitCoord - outPartHorSize,
                                      X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                                      Y_InitCoord + scaledThreadLength,
                                      Color.Black,
                                      0.15f,
                                      SvgLengthUnits.Pixels);

                    svgElements.Add(threadAxialLineSecond); // Make top left part of axial thread's line of anchor
                }
                else
                {
                    rectThreadBodyAnchorSecond = GetSvgRectElement(X_InitCoord - bendLengthMax,
                                    Y_InitCoord,
                                    anchor.ThreadDiameter,
                                    scaledThreadLength,
                                    Color.Transparent,
                                    Color.Black,
                                    1.5f,
                                    SvgLengthUnits.Pixels);

                    svgElements.Add(rectThreadBodyAnchorSecond);

                    rectThreadStepBodyAnchorSecond = GetSvgRectElement(X_InitCoord + anchor.ThreadStep / 2 - bendLengthMax,
                                    Y_InitCoord,
                                    anchor.ThreadDiameter - anchor.ThreadStep,
                                    scaledThreadLength,
                                    Color.Transparent,
                                    Color.Black,
                                    1f,
                                    SvgLengthUnits.Pixels);

                    svgElements.Add(rectThreadStepBodyAnchorSecond); // Make top left part of axial thread's line of anchor

                    threadAxialLineSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                                    Y_InitCoord - outPartHorSize,
                                    X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                                    Y_InitCoord + scaledThreadLength,
                                    Color.Black,
                                    0.15f,
                                    SvgLengthUnits.Pixels);

                    svgElements.Add(threadAxialLineSecond); // Make top left part of axial thread's line of anchor
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
                           X_InitCoord + anchor.ThreadDiameter + 105,
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


                svgElements.Add(GetSvgTextElement($"М{anchor.ThreadDiameter}x{anchor.ThreadStep}",
                                X_InitCoord + anchor.ThreadDiameter + 8,
                                Y_InitCoord - (outPartHorSize - 3),
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
                               Y_InitCoord + scaledThreadLength,
                               X_InitCoord + anchor.ThreadDiameter + (outPartHorSize + 5),
                               Y_InitCoord + scaledThreadLength,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                svgElements.Add(lineHorBotSizeLengthThread);

                var lineVerSizeDiamThread = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord,
                                     X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                     Y_InitCoord + scaledThreadLength,
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
                                      Y_InitCoord + scaledThreadLength,
                                      X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize,
                                      Y_InitCoord + scaledThreadLength,
                                      Color.Black,
                                      0.5f,
                                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeDiamThread);

                svgElements.Add(GetSvgTextElement($"{anchor.ThreadLength}",
                    X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + outPartHorSize - 2,
                    Y_InitCoord + scaledThreadLength / 2 + 10,
                    -90,
                    SvgLengthUnits.Pixels));    // Make text of size's value length of thread
            }

            // Size of anchors's diametr

            var lineHorSizeDiamAnchor = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + scaledThreadLength + outPartHorSize,
                       X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 55,
                       Y_InitCoord + scaledThreadLength + outPartHorSize,
                       Color.Black,
                       0.5f,
                       SvgLengthUnits.Pixels);

            svgElements.Add(lineHorSizeDiamAnchor);

            var lineSerifLeftSizeDiamAnchor = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + scaledThreadLength + outPartHorSize,
                      X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                      Y_InitCoord + scaledThreadLength + outPartHorSize,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifLeftSizeDiamAnchor);

            var lineSerifRightSizeDiamAnchor = GetSerif(X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + scaledThreadLength + outPartHorSize,
                     X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2,
                     Y_InitCoord + scaledThreadLength + outPartHorSize,
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

            svgElements.Add(lineSerifRightSizeDiamAnchor);

            svgElements.Add(GetSvgTextElement($"{iconDiameter}{anchor.Diameter}",
                X_InitCoord + anchor.ThreadDiameter / 2 + anchor.Diameter / 2 + 5,
                Y_InitCoord + scaledThreadLength + outPartHorSize - 2,
                0,
                SvgLengthUnits.Pixels));    // Make text of size's value diametr of anchor

            //Make objects basic part without thread and bend 

            SvgRectElement rectBasicBodyAnchor;
            SvgRectElement rectBasicBodyAnchorSecond;

            //Make objects of sizes anchor's length

            SvgLineElement lineHorTopSizeLengthOfAnchor;
            SvgLineElement lineHorBotSizeLengthOfAnchor;
            SvgLineElement lineVertSizeLengthOfAnchor;

            SvgLineElement lineHorTopSizeLengthOfAnchorWithoutRadius;
            SvgLineElement lineHorBotSizeLengthOfAnchorWithoutRadius;
            SvgLineElement lineVertSizeLengthOfAnchorWithoutRadius;

            //Make object bending part without radius

            SvgRectElement rectBendAnchor;

            //Make objects bending part with radius

            var pbRadiusBend = new SvgPathBuilder();
            var pathRadiusBend = new SvgPathElement();
            var pbRadiusBendSecond = new SvgPathBuilder();
            var pathRadiusBendSecond = new SvgPathElement();

            SvgLineElement basicBodyTopAxialLineFirst;
            SvgLineElement basicBodyTopAxialLineSecond;
            SvgLineElement middleAxialLine;

            SvgLineElement bendPartWithoutRadiusAxialLineRight;
            SvgLineElement bendPartWithoutRadiusAxialLineLeft;

            SvgLineElement basicBodyBotAxialLineFirst;
            SvgLineElement basicBodyBotAxialLineSecond;

            SvgPathBuilder pbAxialBendRadiusRightfOfAnchor = new();
            SvgPathElement pathAxialBendRadiusRightfOfAnchor = new();

            SvgPathBuilder pbAxialBendRadiusLeftfOfAnchor = new();
            SvgPathElement pathAxialBendRadiusLeftfOfAnchor = new();

            if (anchor.Length <= lengthMax)
            {            
                basicBodyTopAxialLineFirst = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                                   Y_InitCoord + scaledThreadLength,
                                   X_InitCoord + anchor.ThreadDiameter / 2,
                                   Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                                   Color.Black,
                                   0.15f,
                                   SvgLengthUnits.Pixels); // drawing of axial line in right side of anchor 

                svgElements.Add(basicBodyTopAxialLineFirst);

                pbAxialBendRadiusRightfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                        Y_InitCoord + anchor.Length - (anchor.Diameter + anchor.BendRadius));
                pbAxialBendRadiusRightfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                        Y_InitCoord + anchor.Length - (anchor.Diameter + anchor.BendRadius),
                        X_InitCoord + anchor.ThreadDiameter / 2,
                        Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + anchor.Length - anchor.Diameter / 2);

                pathAxialBendRadiusRightfOfAnchor.PathData = pbAxialBendRadiusRightfOfAnchor.ToPathData();
                pathAxialBendRadiusRightfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                pathAxialBendRadiusRightfOfAnchor.Stroke = new SvgPaint(Color.Black);
                pathAxialBendRadiusRightfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                svgElements.Add(pathAxialBendRadiusRightfOfAnchor); // drawing of axial line in right bend radius of anchor 

                //Draw basic part without thread and bend 

                rectBasicBodyAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + scaledThreadLength,
                    anchor.Diameter,
                    anchor.Length - (scaledThreadLength + anchor.BendRadius + anchor.Diameter),
                    Color.Transparent,
                    Color.Black,
                    1.5f,
                    SvgLengthUnits.Pixels);

                if (anchor.BendLength <= bendLengthMax)
                {
                    pbAxialBendRadiusLeftfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                       Y_InitCoord + anchor.Length - (anchor.Diameter + anchor.BendRadius));
                    pbAxialBendRadiusLeftfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                            Y_InitCoord + anchor.Length - (anchor.Diameter + anchor.BendRadius),
                            X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                            Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                            X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter + anchor.Diameter / 2 + anchor.BendRadius,
                            Y_InitCoord + anchor.Length - anchor.Diameter / 2);

                    pathAxialBendRadiusLeftfOfAnchor.PathData = pbAxialBendRadiusLeftfOfAnchor.ToPathData();
                    pathAxialBendRadiusLeftfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathAxialBendRadiusLeftfOfAnchor.Stroke = new SvgPaint(Color.Black);
                    pathAxialBendRadiusLeftfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                    svgElements.Add(pathAxialBendRadiusLeftfOfAnchor); // drawing of axial line in left bend radius of anchor 

                    basicBodyTopAxialLineSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                                   Y_InitCoord + scaledThreadLength,
                                   X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                                   Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                                   Color.Black,
                                   0.15f,
                                   SvgLengthUnits.Pixels); // drawing of axial line in left side of anchor 

                    svgElements.Add(basicBodyTopAxialLineSecond);

                    //Draw second basic part without thread and bend 

                    rectBasicBodyAnchorSecond = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength,
                        anchor.Diameter,
                        anchor.Length - (scaledThreadLength + anchor.BendRadius + anchor.Diameter),
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

                    bendPartWithoutRadiusAxialLineRight = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                                   Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                                   X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.BendRadius - (anchor.BendLength - 2 * anchor.Diameter),
                                   Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                                   Color.Black,
                                   0.15f,
                                   SvgLengthUnits.Pixels); // drawing of axial line of bend part without radius

                    svgElements.Add(bendPartWithoutRadiusAxialLineRight);

                    middleAxialLine = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter - anchor.BendLength / 2,
                                   Y_InitCoord - outPartHorSize,
                                   X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter - anchor.BendLength / 2,
                                   Y_InitCoord + anchor.Length + 5,
                                   Color.Black,
                                   0.15f,
                                   SvgLengthUnits.Pixels); // drawing of axial line in middle

                    svgElements.Add(middleAxialLine);

                    //Size of bending part

                    var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                          X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + anchor.Length + (outPartHorSize + 5) + outPartHorSize,
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPart);

                    var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.Length + (outPartHorSize + 5) + outPartHorSize,
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPart);

                    var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPart);

                    var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPart);

                    var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPart);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                          Y_InitCoord + anchor.Length + outPartHorSize - 2 + outPartHorSize,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of thread

                    //Size of bending part without radius

                    var lineVertLeftSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter + anchor.BendRadius,
                          Y_InitCoord + anchor.Length,
                          X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter + anchor.BendRadius,
                          Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPartWithoutRadius);

                    var lineVertRightSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                         Y_InitCoord + anchor.Length,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                         Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPartWithoutRadius);

                    var lineHorSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter + anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.BendRadius - anchor.Diameter,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPartWithoutRadius);

                    var lineSerifLeftSizeBendPartWithoutRadius = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.BendRadius + anchor.Diameter,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.BendRadius + anchor.Diameter,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPartWithoutRadius);

                    var lineSerifRightSizeBendPartWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPartWithoutRadius);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength - 2 * (anchor.Diameter + anchor.BendRadius)}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                          Y_InitCoord + anchor.Length + outPartHorSize - 2,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size of bending part without radius

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
                    pbAxialBendRadiusLeftfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                            Y_InitCoord + anchor.Length - (anchor.Diameter + anchor.BendRadius));
                    pbAxialBendRadiusLeftfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                            Y_InitCoord + anchor.Length - (anchor.Diameter + anchor.BendRadius),
                            X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                            Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                            X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax + anchor.Diameter / 2 + anchor.BendRadius,
                            Y_InitCoord + anchor.Length - anchor.Diameter / 2);

                    pathAxialBendRadiusLeftfOfAnchor.PathData = pbAxialBendRadiusLeftfOfAnchor.ToPathData();
                    pathAxialBendRadiusLeftfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathAxialBendRadiusLeftfOfAnchor.Stroke = new SvgPaint(Color.Black);
                    pathAxialBendRadiusLeftfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                    svgElements.Add(pathAxialBendRadiusLeftfOfAnchor); // drawing of axial line in left bend radius of anchor 

                    basicBodyTopAxialLineSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                                   Y_InitCoord + scaledThreadLength,
                                   X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                                   Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                                   Color.Black,
                                   0.15f,
                                   SvgLengthUnits.Pixels); // drawing of axial line in left side of anchor 

                    svgElements.Add(basicBodyTopAxialLineSecond);

                    middleAxialLine = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter / 2 - bendLengthMax / 2,
                                  Y_InitCoord - outPartHorSize,
                                  X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter / 2 - bendLengthMax / 2,
                                  Y_InitCoord + anchor.Length + 5,
                                  Color.Black,
                                  0.15f,
                                  SvgLengthUnits.Pixels); // drawing of axial line in middle

                    svgElements.Add(middleAxialLine);

                    //Draw second basic part without thread and bend 

                    rectBasicBodyAnchorSecond = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                        Y_InitCoord + scaledThreadLength,
                        anchor.Diameter,
                        anchor.Length - (scaledThreadLength + anchor.BendRadius + anchor.Diameter),
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

                    bendPartWithoutRadiusAxialLineRight = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                          Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                          X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                          Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                          Color.Black,
                          0.15f,
                          SvgLengthUnits.Pixels); // drawing of axial line of bend right part without radius

                    svgElements.Add(bendPartWithoutRadiusAxialLineRight);

                    bendPartWithoutRadiusAxialLineLeft = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.BendRadius + anchor.Diameter,
                       Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                       X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                       Y_InitCoord + anchor.Length - anchor.Diameter / 2,
                       Color.Black,
                       0.15f,
                       SvgLengthUnits.Pixels); // drawing of axial line of bend left part without radius

                    svgElements.Add(bendPartWithoutRadiusAxialLineLeft);

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
                          Y_InitCoord + anchor.Length + (outPartHorSize + 5) + outPartHorSize,
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPart);

                    var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + anchor.Length + (outPartHorSize + 5) + outPartHorSize,
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPart);

                    var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPart);

                    var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPart);

                    var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + anchor.Length + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPart);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                          Y_InitCoord + anchor.Length + outPartHorSize - 2 + outPartHorSize,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of thread

                    //Size of bending part without radius

                    var lineVertLeftSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter + anchor.BendRadius,
                          Y_InitCoord + anchor.Length,
                          X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter + anchor.BendRadius,
                          Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPartWithoutRadius);

                    var lineVertRightSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                         Y_InitCoord + anchor.Length,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                         Y_InitCoord + anchor.Length + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPartWithoutRadius);

                    var lineHorSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter + anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPartWithoutRadius);

                    var lineSerifLeftSizeBendPartWithoutRadius = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter + anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter + anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPartWithoutRadius);

                    var lineSerifRightSizeBendPartWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - anchor.Diameter - anchor.BendRadius,
                               Y_InitCoord + anchor.Length + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPartWithoutRadius);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength - 2 * (anchor.BendRadius + anchor.Diameter)}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                          Y_InitCoord + anchor.Length + outPartHorSize - 2,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size of bending part without radius

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
                    Y_InitCoord + anchor.Length - anchor.Diameter - anchor.BendRadius - 2,
                    0,
                    SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor



                svgElements.Add(rectBasicBodyAnchor);

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize) + outPartHorSize,
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchor);


                lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                 Y_InitCoord + anchor.Length,
                                 X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize) + outPartHorSize,
                                 Y_InitCoord + anchor.Length,
                                 Color.Black,
                                 0.5f,
                                 SvgLengthUnits.Pixels);

                svgElements.Add(lineHorBotSizeLengthOfAnchor);

                lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                            Y_InitCoord + anchor.Length,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchor);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);

                var lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                            Y_InitCoord + anchor.Length,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                            Y_InitCoord + anchor.Length,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2 + outPartHorSize,
                          Y_InitCoord + anchor.Length / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor

                // Size of anchors's length without radius

                lineHorTopSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchorWithoutRadius);


                lineHorBotSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                 Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                                 X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                 Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                                 Color.Black,
                                 0.5f,
                                 SvgLengthUnits.Pixels);

                svgElements.Add(lineHorBotSizeLengthOfAnchorWithoutRadius);

                lineVertSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchorWithoutRadius);

                var lineSerifTopSizeLengthOfAnchorWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord,
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchorWithoutRadius);

                var lineSerifBotSizeLengthOfAnchorWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                            Y_InitCoord + anchor.Length - (anchor.BendRadius + anchor.Diameter),
                      Color.Black,
                      0.5f,
                      SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchorWithoutRadius);

                svgElements.Add(GetSvgTextElement($"{anchor.Length - (anchor.BendRadius + anchor.Diameter)}",
                          X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2,
                          Y_InitCoord + (anchor.Length - (anchor.BendRadius + anchor.Diameter)) / 2 + 10,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value length of anchor without radius
            }
            else
            {
                pbAxialBendRadiusRightfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                       Y_InitCoord + lengthMax + scaledThreadLength - (anchor.Diameter + anchor.BendRadius));
                pbAxialBendRadiusRightfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength - (anchor.Diameter + anchor.BendRadius),
                        X_InitCoord + anchor.ThreadDiameter / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2);

                pathAxialBendRadiusRightfOfAnchor.PathData = pbAxialBendRadiusRightfOfAnchor.ToPathData();
                pathAxialBendRadiusRightfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                pathAxialBendRadiusRightfOfAnchor.Stroke = new SvgPaint(Color.Black);
                pathAxialBendRadiusRightfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                svgElements.Add(pathAxialBendRadiusRightfOfAnchor); // drawing of axial line in right bend radius of anchor 

                basicBodyTopAxialLineFirst = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                                  Y_InitCoord + scaledThreadLength,
                                  X_InitCoord + anchor.ThreadDiameter / 2,
                                  Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                                  Color.Black,
                                  0.15f,
                                  SvgLengthUnits.Pixels); // drawing of axial line in right side of anchor 

                svgElements.Add(basicBodyTopAxialLineFirst);

                basicBodyBotAxialLineFirst = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord + scaledThreadLength + lengthMax / 2,
                                X_InitCoord + anchor.ThreadDiameter / 2,
                                Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                                Color.Black,
                                0.15f,
                                SvgLengthUnits.Pixels); // drawing of axial line in right bottom side of anchor 

                svgElements.Add(basicBodyBotAxialLineFirst);

                //Draw basic part without thread and bend 

                //Make top half basic part without thread and bend

                var pbHalfTopBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfTopBasicBodyAnchor = new SvgPathElement();

                pbHalfTopBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength);
                pbHalfTopBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                pbHalfTopBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

                pathHalfTopBasicBodyAnchor.PathData = pbHalfTopBasicBodyAnchor.ToPathData();
                pathHalfTopBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                pathHalfTopBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                pathHalfTopBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathHalfTopBasicBodyAnchor);

                // Make gap Top Line

                var pbgapTop = new SvgPathBuilder();
                var pathgapTop = new SvgPathElement();
                float halfDiam;

                if (anchor.ThreadLength > 0)
                    halfDiam = anchor.Diameter - anchor.ThreadDiameter / 2;
                else
                    halfDiam = 0;

                pbgapTop.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                       Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                pbgapTop.AddCurveTo(false, X_InitCoord + halfDiam,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap) - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

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
                       Y_InitCoord + scaledThreadLength + lengthMax / 2);
                pbgapBot.AddCurveTo(false, X_InitCoord + halfDiam,
                    Y_InitCoord + scaledThreadLength + lengthMax / 2 - 5,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + scaledThreadLength + lengthMax / 2,
                    X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2,
                    Y_InitCoord + scaledThreadLength + lengthMax / 2);

                pathgapBot.PathData = pbgapBot.ToPathData();
                pathgapBot.Fill = new SvgPaint(Color.Transparent);
                pathgapBot.Stroke = new SvgPaint(Color.Black);
                pathgapBot.StrokeWidth = new SvgLength(0.5f);

                svgElements.Add(pathgapBot);           

                var pbHalfBotBasicBodyAnchor = new SvgPathBuilder();
                var pathHalfBotBasicBodyAnchor = new SvgPathElement();

                //Make bottom half basic part without thread and bend

                pbHalfBotBasicBodyAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                                Y_InitCoord + scaledThreadLength + lengthMax / 2);
                pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter);
                pbHalfBotBasicBodyAnchor.AddHorizontalLineTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2);
                pbHalfBotBasicBodyAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax / 2);

                pathHalfBotBasicBodyAnchor.PathData = pbHalfBotBasicBodyAnchor.ToPathData();
                pathHalfBotBasicBodyAnchor.Fill = new SvgPaint(Color.Transparent);
                pathHalfBotBasicBodyAnchor.Stroke = new SvgPaint(Color.Black);
                pathHalfBotBasicBodyAnchor.StrokeWidth = new SvgLength(1.5f);

                svgElements.Add(pathHalfBotBasicBodyAnchor);

                //Draw bending part with radius 

                pbRadiusBend.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + lengthMax + scaledThreadLength - anchor.BendRadius - anchor.Diameter);
                pbRadiusBend.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2,
                    Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter,
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                    Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter,
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                    Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                pbRadiusBend.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax);
                pbRadiusBend.AddCurveTo(false, X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                    Y_InitCoord + lengthMax + scaledThreadLength,
                    X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                    Y_InitCoord + lengthMax + scaledThreadLength - anchor.BendRadius - anchor.Diameter,
                    X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter) / 2,
                    Y_InitCoord + lengthMax + scaledThreadLength - anchor.BendRadius - anchor.Diameter);

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
                    pbAxialBendRadiusLeftfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                           Y_InitCoord + lengthMax + scaledThreadLength - (anchor.Diameter + anchor.BendRadius));
                    pbAxialBendRadiusLeftfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                            Y_InitCoord + lengthMax + scaledThreadLength - (anchor.Diameter + anchor.BendRadius),
                            X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                            X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter / 2 + anchor.BendRadius + anchor.Diameter,
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2);

                    pathAxialBendRadiusLeftfOfAnchor.PathData = pbAxialBendRadiusLeftfOfAnchor.ToPathData();
                    pathAxialBendRadiusLeftfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathAxialBendRadiusLeftfOfAnchor.Stroke = new SvgPaint(Color.Black);
                    pathAxialBendRadiusLeftfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                    svgElements.Add(pathAxialBendRadiusLeftfOfAnchor); // drawing of axial line in left bend radius of anchor

                    basicBodyTopAxialLineSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                         Y_InitCoord + scaledThreadLength,
                         X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                         Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                         Color.Black,
                         0.15f,
                         SvgLengthUnits.Pixels); // drawing of axial line in left side of anchor 

                    svgElements.Add(basicBodyTopAxialLineSecond);

                    bendPartWithoutRadiusAxialLineRight = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                    Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.BendRadius - (anchor.BendLength - 2 * anchor.Diameter),
                    Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                    Color.Black,
                    0.15f,
                    SvgLengthUnits.Pixels); // drawing of axial line of bend part without radius

                    svgElements.Add(bendPartWithoutRadiusAxialLineRight);

                    basicBodyBotAxialLineSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                                    Y_InitCoord + scaledThreadLength + lengthMax / 2,
                                    X_InitCoord + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                                    Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                                    Color.Black,
                                    0.15f,
                                    SvgLengthUnits.Pixels); // drawing of axial line in left bottom side of anchor 

                    svgElements.Add(basicBodyBotAxialLineSecond);

                    //Draw second basic part without thread and bend 

                    //Make bottom half basic part without thread and bend

                    pbHalfBotBasicBodySecondAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                                    Y_InitCoord + scaledThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter);
                    pbHalfBotBasicBodySecondAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + 2 * anchor.Diameter);
                    pbHalfBotBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodySecondAnchor.PathData = pbHalfBotBasicBodySecondAnchor.ToPathData();
                    pathHalfBotBasicBodySecondAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodySecondAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodySecondAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodySecondAnchor);

                    // Make gap Bot Line

                    pbgapBotSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                           Y_InitCoord + scaledThreadLength + lengthMax / 2);
                    pbgapBotSecond.AddCurveTo(false, X_InitCoord + halfDiam - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2 - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2);

                    pathgapBotSecond.PathData = pbgapBotSecond.ToPathData();
                    pathgapBotSecond.Fill = new SvgPaint(Color.Transparent);
                    pathgapBotSecond.Stroke = new SvgPaint(Color.Black);
                    pathgapBotSecond.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapBotSecond);

                    //Make top half basic part without thread and bend

                    pbHalfTopBasicBodySecondAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                    pbHalfTopBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength);
                    pbHalfTopBasicBodySecondAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + 2 * anchor.Diameter);
                    pbHalfTopBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

                    pathHalfTopBasicBodySecondAnchor.PathData = pbHalfTopBasicBodySecondAnchor.ToPathData();
                    pathHalfTopBasicBodySecondAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfTopBasicBodySecondAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfTopBasicBodySecondAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfTopBasicBodySecondAnchor);

                    // Make gap Top Line

                    pbgapTopSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength + anchor.Diameter,
                           Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                    pbgapTopSecond.AddCurveTo(false, X_InitCoord + halfDiam - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap) - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

                    pathgapTopSecond.PathData = pbgapTopSecond.ToPathData();
                    pathgapTopSecond.Fill = new SvgPaint(Color.Transparent);
                    pathgapTopSecond.Stroke = new SvgPaint(Color.Black);
                    pathgapTopSecond.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapTopSecond);

                    middleAxialLine = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength / 2 + anchor.Diameter,
                             Y_InitCoord - outPartHorSize,
                             X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendLength / 2 + anchor.Diameter,
                             Y_InitCoord + scaledThreadLength + lengthMax + 5,
                             Color.Black,
                             0.15f,
                             SvgLengthUnits.Pixels); // drawing of axial line in middle

                    svgElements.Add(middleAxialLine);

                    //Draw bending part without radius

                    rectBendAnchor = GetSvgRectElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter,
                        anchor.BendLength - (anchor.BendRadius + anchor.Diameter) * 2,
                        anchor.Diameter,
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);

                    svgElements.Add(rectBendAnchor);

                    //Size of bending part

                    var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter,
                          X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                          Y_InitCoord + scaledThreadLength + lengthMax + (outPartHorSize + 5) + outPartHorSize,
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPart);

                    var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + scaledThreadLength + lengthMax + (outPartHorSize + 5) + outPartHorSize,
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPart);

                    var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPart);

                    var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                               X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPart);

                    var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPart);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                          Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize - 2 + outPartHorSize,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size of bending part

                    //Size of bending part without radius

                    var lineVertLeftSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + (anchor.BendRadius + anchor.Diameter),
                          Y_InitCoord + scaledThreadLength + lengthMax,
                          X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + (anchor.BendRadius + anchor.Diameter),
                          Y_InitCoord + scaledThreadLength + lengthMax + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPartWithoutRadius);

                    var lineVertRightSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                         Y_InitCoord + scaledThreadLength + lengthMax,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                         Y_InitCoord + scaledThreadLength + lengthMax + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPartWithoutRadius);

                    var lineHorSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + anchor.Diameter + anchor.BendRadius,
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPartWithoutRadius);

                    var lineSerifLeftSizeBendPartWithoutRadius = GetSerif(X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - anchor.BendLength + (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPartWithoutRadius);

                    var lineSerifRightSizeBendPartWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPartWithoutRadius);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength - 2 * (anchor.Diameter + anchor.BendRadius)}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - anchor.BendLength) / 2 - 10,
                          Y_InitCoord + scaledThreadLength + lengthMax + outPartHorSize - 2,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size of bending part without radius

                    //Draw second bending part with radius 

                    pbRadiusBendSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength,
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength,
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength,
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius);
                    pbRadiusBendSecond.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength - anchor.Diameter);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength - anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + scaledThreadLength + lengthMax,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + 2 * anchor.Diameter - anchor.BendLength + anchor.BendRadius,
                        Y_InitCoord + scaledThreadLength + lengthMax);


                    pathRadiusBendSecond.PathData = pbRadiusBendSecond.ToPathData();
                    pathRadiusBendSecond.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBendSecond.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBendSecond.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBendSecond);
                }
                else
                {
                    pbAxialBendRadiusLeftfOfAnchor.AddMoveTo(false, X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                           Y_InitCoord + lengthMax + scaledThreadLength - (anchor.Diameter + anchor.BendRadius));
                    pbAxialBendRadiusLeftfOfAnchor.AddCurveTo(false, X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                            Y_InitCoord + lengthMax + scaledThreadLength - (anchor.Diameter + anchor.BendRadius),
                            X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                            X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax + anchor.Diameter / 2 + anchor.BendRadius,
                            Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2);

                    pathAxialBendRadiusLeftfOfAnchor.PathData = pbAxialBendRadiusLeftfOfAnchor.ToPathData();
                    pathAxialBendRadiusLeftfOfAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathAxialBendRadiusLeftfOfAnchor.Stroke = new SvgPaint(Color.Black);
                    pathAxialBendRadiusLeftfOfAnchor.StrokeWidth = new SvgLength(0.15f);

                    svgElements.Add(pathAxialBendRadiusLeftfOfAnchor); // drawing of axial line in left bend radius of anchor

                    basicBodyTopAxialLineSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                       Y_InitCoord + scaledThreadLength,
                       X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                       Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                       Color.Black,
                       0.15f,
                       SvgLengthUnits.Pixels); // drawing of axial line in left side of anchor 

                    svgElements.Add(basicBodyTopAxialLineSecond);

                    basicBodyBotAxialLineSecond = GetSvgLineElement(X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                                    Y_InitCoord + scaledThreadLength + lengthMax / 2,
                                    X_InitCoord + anchor.ThreadDiameter / 2 - bendLengthMax,
                                    Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                                    Color.Black,
                                    0.15f,
                                    SvgLengthUnits.Pixels); // drawing of axial line in left bottom side of anchor 

                    svgElements.Add(basicBodyBotAxialLineSecond);

                    bendPartWithoutRadiusAxialLineRight = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                         Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                         X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                         Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                         Color.Black,
                         0.15f,
                         SvgLengthUnits.Pixels); // drawing of axial line of bend right part without radius

                    svgElements.Add(bendPartWithoutRadiusAxialLineRight);

                    bendPartWithoutRadiusAxialLineLeft = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.BendRadius + anchor.Diameter,
                       Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                       X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                       Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                       Color.Black,
                       0.15f,
                       SvgLengthUnits.Pixels); // drawing of axial line of bend left part without radius

                    svgElements.Add(bendPartWithoutRadiusAxialLineLeft);


                    //Draw second basic part without thread and bend 

                    //Make bottom half basic part without thread and bend

                    pbHalfBotBasicBodySecondAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                                    Y_InitCoord + scaledThreadLength + lengthMax / 2);
                    pbHalfBotBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius - anchor.Diameter);
                    pbHalfBotBasicBodySecondAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter);
                    pbHalfBotBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + lengthMax / 2);

                    pathHalfBotBasicBodySecondAnchor.PathData = pbHalfBotBasicBodySecondAnchor.ToPathData();
                    pathHalfBotBasicBodySecondAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfBotBasicBodySecondAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfBotBasicBodySecondAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfBotBasicBodySecondAnchor);

                    // Make gap Bot Line

                    pbgapBotSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                           Y_InitCoord + scaledThreadLength + lengthMax / 2);
                    pbgapBotSecond.AddCurveTo(false, X_InitCoord + halfDiam - bendLengthMax,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2 - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - bendLengthMax,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - bendLengthMax,
                        Y_InitCoord + scaledThreadLength + lengthMax / 2);

                    pathgapBotSecond.PathData = pbgapBotSecond.ToPathData();
                    pathgapBotSecond.Fill = new SvgPaint(Color.Transparent);
                    pathgapBotSecond.Stroke = new SvgPaint(Color.Black);
                    pathgapBotSecond.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapBotSecond);

                    //Make top half basic part without thread and bend

                    pbHalfTopBasicBodySecondAnchor.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                    pbHalfTopBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength);
                    pbHalfTopBasicBodySecondAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter);
                    pbHalfTopBasicBodySecondAnchor.AddVerticalLineTo(false, Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

                    pathHalfTopBasicBodySecondAnchor.PathData = pbHalfTopBasicBodySecondAnchor.ToPathData();
                    pathHalfTopBasicBodySecondAnchor.Fill = new SvgPaint(Color.Transparent);
                    pathHalfTopBasicBodySecondAnchor.Stroke = new SvgPaint(Color.Black);
                    pathHalfTopBasicBodySecondAnchor.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathHalfTopBasicBodySecondAnchor);

                    // Make gap Top Line

                    pbgapTopSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                           Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));
                    pbgapTopSecond.AddCurveTo(false, X_InitCoord + halfDiam - bendLengthMax,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap) - 5,
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - bendLengthMax,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap),
                        X_InitCoord + anchor.Diameter / 2 + anchor.ThreadDiameter / 2 - bendLengthMax,
                        Y_InitCoord + scaledThreadLength + (lengthMax / 2 - gap));

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
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                    pbHalfRightBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius);
                    pbHalfRightBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + scaledThreadLength);
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
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                    pbHalfLeftBendPartAnchor.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.BendRadius + anchor.Diameter);
                    pbHalfLeftBendPartAnchor.AddVerticalLineTo(false, Y_InitCoord + lengthMax + scaledThreadLength);
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
                           Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                    pbgapRight.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap - 5,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                        Y_InitCoord + lengthMax + scaledThreadLength,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 + gap,
                        Y_InitCoord + lengthMax + scaledThreadLength);

                    pathgapRight.PathData = pbgapRight.ToPathData();
                    pathgapRight.Fill = new SvgPaint(Color.Transparent);
                    pathgapRight.Stroke = new SvgPaint(Color.Black);
                    pathgapRight.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapRight);

                    // Make gap Left Line

                    var pbgapLeft = new SvgPathBuilder();
                    var pathgapLeft = new SvgPathElement();

                    pbgapLeft.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter);
                    pbgapLeft.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2 - 5,
                        Y_InitCoord + lengthMax + scaledThreadLength - anchor.Diameter / 2,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax / 2,
                        Y_InitCoord + lengthMax + scaledThreadLength);

                    pathgapLeft.PathData = pbgapLeft.ToPathData();
                    pathgapLeft.Fill = new SvgPaint(Color.Transparent);
                    pathgapLeft.Stroke = new SvgPaint(Color.Black);
                    pathgapLeft.StrokeWidth = new SvgLength(0.5f);

                    svgElements.Add(pathgapLeft);

                    //Size of bending part 

                    var lineVertLeftSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + lengthMax + scaledThreadLength - anchor.BendRadius - anchor.Diameter,
                          X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                          Y_InitCoord + lengthMax + scaledThreadLength + (outPartHorSize + 5) + outPartHorSize,
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPart);

                    var lineVertRightSizeBendPart = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + lengthMax + scaledThreadLength - (anchor.BendRadius + anchor.Diameter),
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                         Y_InitCoord + lengthMax + scaledThreadLength + (outPartHorSize + 5) + outPartHorSize,
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPart);

                    var lineHorSizeBendPart = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPart);

                    var lineSerifLeftSizeBendPart = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                               X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPart);       

                    var lineSerifRightSizeBendPart = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPart);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                          Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize - 2 + outPartHorSize,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size of bending part 

                    //Size of bending part without radius

                    var lineVertLeftSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + (anchor.BendRadius + anchor.Diameter),
                          Y_InitCoord + lengthMax + scaledThreadLength,
                          X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + (anchor.BendRadius + anchor.Diameter),
                          Y_InitCoord + lengthMax + scaledThreadLength + (outPartHorSize + 5),
                          Color.Black,
                          0.5f,
                          SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertLeftSizeBendPartWithoutRadius);

                    var lineVertRightSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                         Y_InitCoord + lengthMax + scaledThreadLength,
                         X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                         Y_InitCoord + lengthMax + scaledThreadLength + (outPartHorSize + 5),
                         Color.Black,
                         0.5f,
                         SvgLengthUnits.Pixels);

                    svgElements.Add(lineVertRightSizeBendPartWithoutRadius);

                    var lineHorSizeBendPartWithoutRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineHorSizeBendPartWithoutRadius);

                    var lineSerifLeftSizeBendPartWithoutRadius = GetSerif(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifLeftSizeBendPartWithoutRadius);

                    var lineSerifRightSizeBendPartWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                               Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize,
                               Color.Black,
                               0.5f,
                               SvgLengthUnits.Pixels);

                    svgElements.Add(lineSerifRightSizeBendPartWithoutRadius);

                    svgElements.Add(GetSvgTextElement($"{anchor.BendLength - 2 * (anchor.BendRadius + anchor.Diameter)}",
                          X_InitCoord + (anchor.Diameter + anchor.ThreadDiameter - bendLengthMax) / 2 - 10,
                          Y_InitCoord + lengthMax + scaledThreadLength + outPartHorSize - 2,
                          0,
                          SvgLengthUnits.Pixels));    // Make text of size of bending part without radius


                    middleAxialLine = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter / 2 - bendLengthMax / 2,
                     Y_InitCoord - outPartHorSize,
                     X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 + anchor.Diameter / 2 - bendLengthMax / 2,
                     Y_InitCoord + scaledThreadLength + lengthMax + 5,
                     Color.Black,
                     0.15f,
                     SvgLengthUnits.Pixels); // drawing of axial line in middle

                    svgElements.Add(middleAxialLine);

                    //Draw second bending part with radius 

                    pbRadiusBendSecond.AddMoveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.BendRadius + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius);
                    pbRadiusBendSecond.AddHorizontalLineTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax);
                    pbRadiusBendSecond.AddCurveTo(false, X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                        Y_InitCoord + scaledThreadLength + lengthMax - anchor.Diameter - anchor.BendRadius,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax,
                        Y_InitCoord + scaledThreadLength + lengthMax,
                        X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - bendLengthMax + anchor.BendRadius + anchor.Diameter,
                        Y_InitCoord + scaledThreadLength + lengthMax);


                    pathRadiusBendSecond.PathData = pbRadiusBendSecond.ToPathData();
                    pathRadiusBendSecond.Fill = new SvgPaint(Color.Transparent);
                    pathRadiusBendSecond.Stroke = new SvgPaint(Color.Black);
                    pathRadiusBendSecond.StrokeWidth = new SvgLength(1.5f);

                    svgElements.Add(pathRadiusBendSecond);
                }

                //Size of radius

                var lineInclinSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                     Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                     X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                     Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                     Color.Black,
                     0.5f,
                     SvgLengthUnits.Pixels);

                svgElements.Add(lineInclinSizeRadius);

                var lineHorSizeRadius = GetSvgLineElement(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius,
                  Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                  X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                  Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

                svgElements.Add(lineHorSizeRadius);

                var lineSerifSizeRadius = GetSerifRad(X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                  Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                  X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2),
                  Y_InitCoord + scaledThreadLength + lengthMax - anchor.BendRadius * (1 - (float)Math.Sqrt(2) / 2) - anchor.Diameter,
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifSizeRadius);

                svgElements.Add(GetSvgTextElement($"R{anchor.BendRadius}",
                    X_InitCoord - (anchor.Diameter - anchor.ThreadDiameter) / 2 - anchor.BendRadius - outPartRadSize,
                    Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter) - 2,
                    0,
                    SvgLengthUnits.Pixels));    // Make text of size's value radius of anchor

                // Size of anchors's length

                lineHorTopSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize) + outPartHorSize,
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);               

                lineHorBotSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 - (anchor.BendRadius + anchor.Diameter),
                                Y_InitCoord + scaledThreadLength + lengthMax,
                                X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize) + outPartHorSize,
                                Y_InitCoord + scaledThreadLength + lengthMax,
                                Color.Black,
                                0.5f,
                                SvgLengthUnits.Pixels);

                var lineSerifTopSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                           Y_InitCoord,
                           X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                           Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                lineSerifBotSizeLengthOfAnchor = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                        Y_InitCoord + scaledThreadLength + lengthMax,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                        Y_InitCoord + scaledThreadLength + lengthMax,
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

                lineVertSizeLengthOfAnchor = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                        Y_InitCoord,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) + outPartHorSize,
                        Y_InitCoord + scaledThreadLength + lengthMax,
                        Color.Black,
                        0.5f,
                        SvgLengthUnits.Pixels);

                svgElements.Add(GetSvgTextElement($"{anchor.Length}",
                      X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2 + outPartHorSize,
                      Y_InitCoord + (scaledThreadLength + lengthMax) / 2 + 10,
                      -90,
                      SvgLengthUnits.Pixels));    // Make text of size's value length of anchor

                // Size of anchors's length without radius

                lineHorTopSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                            Y_InitCoord,
                            X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                            Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineHorTopSizeLengthOfAnchorWithoutRadius);

                lineHorBotSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2,
                                Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                                X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + outPartRadSize),
                                Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                                Color.Black,
                                0.5f,
                                SvgLengthUnits.Pixels);

                svgElements.Add(lineHorBotSizeLengthOfAnchorWithoutRadius);

                var lineSerifTopSizeLengthOfAnchorWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord,
                           X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                           Y_InitCoord,
                            Color.Black,
                            0.5f,
                            SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifTopSizeLengthOfAnchorWithoutRadius);

                var lineSerifBotSizeLengthOfAnchorWithoutRadius = GetSerif(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                  Color.Black,
                  0.5f,
                  SvgLengthUnits.Pixels);

                svgElements.Add(lineSerifBotSizeLengthOfAnchorWithoutRadius);

                lineVertSizeLengthOfAnchorWithoutRadius = GetSvgLineElement(X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord,
                        X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40),
                        Y_InitCoord + scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter),
                        Color.Black,
                        0.5f,
                        SvgLengthUnits.Pixels);

                svgElements.Add(lineVertSizeLengthOfAnchorWithoutRadius);

                svgElements.Add(GetSvgTextElement($"{anchor.Length - (anchor.Diameter + anchor.BendRadius)}",
                      X_InitCoord + (anchor.ThreadDiameter + anchor.Diameter) / 2 + (outPartHorSize + 40) - 2,
                      Y_InitCoord + (scaledThreadLength + lengthMax - (anchor.BendRadius + anchor.Diameter)) / 2 + 10,
                      -90,
                      SvgLengthUnits.Pixels));    // Make text of size's value length of anchor without radius

                svgElements.Add(lineHorTopSizeLengthOfAnchor);

                svgElements.Add(lineHorBotSizeLengthOfAnchor);

                svgElements.Add(lineSerifBotSizeLengthOfAnchor);

                svgElements.Add(lineVertSizeLengthOfAnchor);               

                svgElements.Add(lineSerifTopSizeLengthOfAnchor);
            }

            // GetDescriptionAnchor(anchor, paramsCanvas, svgElements); 

            for (int i = 0; i < svgElements.Count; i++)
                svgDoc.RootSvg.Children.Insert(i, svgElements[i]);

            SvgViewBox view = new();
            view.MinX = 0;
            view.MinY = 0;
            view.Width = viewWidth;
            view.Height = viewHeight;

            svgDoc.RootSvg.ViewBox = view;

            StringBuilder stringBuilder = new();
            svgDoc.Save(stringBuilder);
            string xml = stringBuilder.ToString();
            string svgElem = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            anchor.SvgElement = svgElem;
        }

        void GetScaledLength(float threadLength, float threadLengthSecond)
        {
            scaledThreadLength = threadLength;
            scaledSecondThreadLength = threadLengthSecond;
            if (threadLength >= threadLengthSecond)
                GetConstraints(threadLength);
            else
                GetConstraints(threadLengthSecond);
            void GetConstraints(float threadLength)
            {
                if (threadLength > 300 && threadLength <= 600)
                {
                    scaledThreadLength /= 2;
                    scaledSecondThreadLength /= 2;
                }
                else if (threadLength > 600)
                {
                    scaledThreadLength /= 3f;
                    scaledSecondThreadLength /= 3f;
                }
            }
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
