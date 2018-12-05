using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Measure
{
    public partial class FrmMeasure : Form
    {
        private IPointCollection pCollection=new MultipointClass();       
        private IGeometry pGeometry;
        double TotalLength, SegLength,LastSeg;
        double TotalAreaLength, SegAreaLength,LastSegArea;
        private IPoint m_CurPoint = new PointClass();
       
        private IArea pArea;
        double myArea, Perimeter;

        public int CheckedBtn = 0;
      
        private struct myUnit
        {
           public  esriUnits pUnit;
            public string UnitName;
        }
        private myUnit inUnit,outUnit;    
        private myUnit inAreaUnit, outAreaUnit;
        private  myUnit PriMapUnit;

        private struct ConvertValue
        {
            public esriUnits units;
            public double value; 
        }
        ConvertValue m_SegLen, m_TotLen, m_Area;
        
        //判断选中要素类型
        int shapeType;

        private IUnitConverter CntUnit = new UnitConverterClass();

        private IHookHelper m_HookHelper;
        private double SumLengths, SumPerimeters, SumAreas;
        private double UnitSumLen, UnitSumPeri, UnitSumArea;

        public FrmMeasure(IHookHelper  myhookhelper )
        {
            InitializeComponent();
            m_HookHelper = myhookhelper;
          
        }

        private void FrmMeasure_Load(object sender, EventArgs e)
        {
            tbtnLine.CheckOnClick = true;     
            tbtnArea.CheckOnClick = true;
            tbtnFeature.CheckOnClick = true;
            tbtnSum.CheckOnClick = true;
            foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                tool.CheckOnClick = true;

            foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
            {
                if (tool.Text ==PriMapUnit.UnitName)
                {
                    tool.Checked = true;
                    break;
                }
            }

            inUnit.pUnit = PriMapUnit.pUnit;
            inUnit.UnitName = PriMapUnit.UnitName;

            foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                tool.CheckOnClick = true;

            int CheckUnit = AreaToolItem.DropDownItems.Count;

            if (PriMapUnit.pUnit == esriUnits.esriDecimalDegrees)
            {
                foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                    tool.Enabled = false;
            }

            else
            {
              
                foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                {
                    if (tool.Text == PriMapUnit.UnitName)
                    {
                        tool.Checked = true;
                        CheckUnit--;
                        break;
                    }
                }
            }

                if (CheckUnit < AreaToolItem.DropDownItems.Count)
                {
                    inAreaUnit.pUnit = PriMapUnit.pUnit;
                    inAreaUnit.UnitName = PriMapUnit.UnitName;
                }
                else
                {
                    inAreaUnit.pUnit = esriUnits.esriUnknownUnits;
                    inAreaUnit.UnitName = "Unkown Units";
                }
           
   
           outUnit.pUnit = inUnit.pUnit;
           outUnit.UnitName = inUnit.UnitName;

           outAreaUnit.pUnit = inAreaUnit.pUnit;
           outAreaUnit.UnitName = inAreaUnit.UnitName;
       
        }

        public void  GetMapUnit(IMap pMap)
        {
            string myMapUnit = pMap.MapUnits.ToString().Remove(0, 4);    
            PriMapUnit.pUnit = pMap.MapUnits;
            PriMapUnit.UnitName = myMapUnit;            
        }
              
        public void frmLineDown(ref IPoint pPoint)
        {
            if (tbtnLine.Checked == true)
            {               
                IPoint pPnt = pPoint;

                object Missing = Type.Missing;
                pCollection.AddPoint(pPnt, ref Missing, ref Missing);

                int PointCnt = pCollection.PointCount;               
                double CntTotal;
              
                if (pCollection.PointCount > 1)
                {
                    IPoint pLast = pCollection.get_Point(PointCnt - 1);
                    IPoint pPre = pCollection.get_Point(PointCnt - 2);

                    SegLength = Math.Sqrt(Math.Pow((pLast.X - pPre.X),2) + Math.Pow((pLast.Y - pPre.Y),2));
                    if (SegLength > 0)
                        LastSeg = SegLength;
                    TotalLength = TotalLength + SegLength;
                   
                    SegLength = CntUnit.ConvertUnits(SegLength, inUnit.pUnit, outUnit.pUnit);
                     CntTotal= CntUnit.ConvertUnits(TotalLength, inUnit.pUnit, outUnit.pUnit);
                     //单位变化
                   m_SegLen.value = SegLength;
                   m_SegLen.units = outUnit.pUnit;
                    m_TotLen.value = CntTotal;
                   m_TotLen.units = outUnit.pUnit;

                    SegLength = Math.Round(SegLength, 6);                   
                   CntTotal= Math.Round(CntTotal, 6);
                   double tempSumLen = SumLengths + CntTotal;

                   UnitSumArea = tempSumLen;

                    txtMessage.Clear();
                    txtMessage.Text = "测量线"+"\r\n线段的长度是：" + SegLength+outUnit.UnitName + "\r\n总长度是：" +CntTotal + outUnit.UnitName;
                    if(tbtnSum.Checked==true)
                    txtMessage.Text  += "\r\n" + "\r\n线段长度之和是：" + tempSumLen+ outUnit.UnitName;

                }
            }
        
        }

        public void frmAreaDown(ref IPoint pPoint)
        {
            if (tbtnArea.Checked == true)
            {               
                IPoint pPnt = pPoint;

                object Missing = Type.Missing;
                pCollection.AddPoint(pPnt, ref Missing, ref Missing);
              
                int PointCnt = pCollection.PointCount;              

                if (PointCnt > 1)
                {
                    IPoint FrmPoint = pCollection.get_Point(PointCnt - 2);
                    IPoint toPoint = pCollection.get_Point(PointCnt - 1);
                    IPoint FirstPoint = pCollection.get_Point(0);
                    SegAreaLength = Math.Sqrt(Math.Pow((FrmPoint.X - toPoint.X), 2) + Math.Pow((FrmPoint.Y - toPoint.Y), 2));
                    if (SegAreaLength > 0)
                        LastSegArea = SegAreaLength;                    

                    TotalAreaLength += SegAreaLength;
                    double tempLength;
                    tempLength = Math.Sqrt(Math.Pow((toPoint.X - FirstPoint.X), 2) + Math.Pow((toPoint.Y - FirstPoint.Y), 2));
                    Perimeter = TotalAreaLength + tempLength;
                    myArea = CaculateArea();
                    double CntArea;
                    double CntPerimeter;
                    double CntSegAreaLength;
                   
                    CntArea = myArea;
                    CntPerimeter = CntUnit.ConvertUnits(Perimeter, inUnit.pUnit, outUnit.pUnit);
                    CntSegAreaLength = CntUnit.ConvertUnits(SegAreaLength, inUnit.pUnit, outUnit.pUnit);
                    CntArea=ConvertToArea(CntArea, inAreaUnit.UnitName, outAreaUnit.UnitName);
                   
                    string AreaUnitMess;
                    if (outAreaUnit.UnitName=="Unkown Units")
                        AreaUnitMess = outAreaUnit.UnitName;
                    else
                        AreaUnitMess = "Square " + outAreaUnit.UnitName;

                    //单位变化
                    m_SegLen.value = CntSegAreaLength;
                    m_SegLen.units = outUnit.pUnit;
                    m_TotLen.value=CntPerimeter;
                    m_TotLen.units = outUnit.pUnit;
                    m_Area.value = CntArea;
                    m_Area.units = outAreaUnit.pUnit;
                   

                    CntPerimeter = Math.Round(CntPerimeter, 6);
                    CntSegAreaLength = Math.Round(CntSegAreaLength, 6);
                    CntArea = Math.Round(CntArea, 6);
                    double tempSumPeri = SumPerimeters + CntPerimeter;
                    double tempSumAreas = SumAreas + CntArea;

                    UnitSumPeri = tempSumPeri;
                    UnitSumArea = tempSumAreas;
                    txtMessage.Text ="测量面"+ "\r\n线段长是" + CntSegAreaLength + outUnit.UnitName + "\r\n周长是：" + CntPerimeter + outUnit.UnitName + "\r\n面积是：" + CntArea+AreaUnitMess;
                    if(tbtnSum.Checked==true)
                    txtMessage.Text += "\r\n" + "\r\n周长长度之和是：" + tempSumPeri+ outUnit.UnitName + "\r\n面积之和是：" +tempSumAreas+ AreaUnitMess;

                }
               
            }
 
        }

        private IFeature FeatureMess(ILayer pLayer, IActiveView pActView,IPoint pPoint)
        {

            ISpatialFilter pSpaFilter = new SpatialFilterClass();
            IEnvelope pEnv = new EnvelopeClass();
            pEnv = pPoint.Envelope;
            IFeatureLayer pFeatLayer = (IFeatureLayer)pLayer;
            IFeatureClass pFeatClass = pFeatLayer.FeatureClass;
            pSpaFilter.Geometry = pEnv;
            //为点建立缓冲区 
            double realWorldDisExtend, sizeOfOnePixel;
            int pixelExtend;
            pixelExtend = pActView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right - pActView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;
            realWorldDisExtend = pActView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            sizeOfOnePixel = realWorldDisExtend / pixelExtend;
            double buffer = 2 * sizeOfOnePixel;
            pEnv.XMax = pPoint.X + buffer;
            pEnv.XMin = pPoint.X - buffer;
            pEnv.YMax = pPoint.Y + buffer;
            pEnv.YMin = pPoint.Y - buffer;

            switch (pFeatClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    pSpaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    pSpaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    pSpaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    break;
            }
            IQueryFilter pQueFilter = (IQueryFilter)pSpaFilter;
            IFeatureCursor pFeatCursor = pFeatLayer.Search(pQueFilter, false);
            IFeature pFeat = pFeatCursor.NextFeature();
            if (pFeat != null)
            {
                IGeometry pGeo = pFeat.Shape;
                switch (pGeo.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        IPoint pFeatPnt = (IPoint)pGeo;

                        double px = CntUnit.ConvertUnits(pFeatPnt.X, inUnit.pUnit, outUnit.pUnit);
                        double py = CntUnit.ConvertUnits(pFeatPnt.Y, inUnit.pUnit, outUnit.pUnit);
                        m_SegLen.value = px;
                        m_SegLen.units = outUnit.pUnit;
                        m_TotLen.value = py;
                        m_TotLen.units = outUnit.pUnit;

                        px = Math.Round(px, 6);
                        py = Math.Round(py, 6);
                        shapeType = 1;

                        txtMessage.Text = "点要素" + "\r\nX:" + px + outUnit.UnitName + "\r\nY:" + py + outUnit.UnitName;
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        IPolyline pFeatLine = (IPolyline)pGeo;
                        double FeatLength = CntUnit.ConvertUnits(pFeatLine.Length, inUnit.pUnit, outUnit.pUnit);
                        m_TotLen.value = FeatLength;
                        m_TotLen.units = outUnit.pUnit;


                        FeatLength = Math.Round(FeatLength, 6);
                        shapeType = 2;
                        SumLengths += FeatLength;

                        UnitSumLen = SumLengths;

                        txtMessage.Text = "线要素" + "\r\n线段长度是：" + FeatLength + outUnit.UnitName;
                        if (tbtnSum.Checked == true)
                            txtMessage.Text += "\r\n" + "\r\n 线段长度之和是：" + SumLengths + outUnit.UnitName;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        IPolygon pFeatPolygon = (IPolygon)pGeo;
                        IArea pFeatArea = (IArea)pFeatPolygon;
                        double pFeatPerimeter = CntUnit.ConvertUnits(pFeatPolygon.Length, inUnit.pUnit, outUnit.pUnit);
                        m_TotLen.value = pFeatPerimeter;
                        m_TotLen.units = outUnit.pUnit;
                        shapeType = 3;

                        pFeatPerimeter = Math.Round(pFeatPerimeter, 6);
                        SumPerimeters += pFeatPerimeter;
                        double parea = pFeatArea.Area;
                        double Cntparea = parea;
                        string AreaUnitMess;
                        Cntparea = ConvertToArea(Cntparea, inAreaUnit.UnitName, outAreaUnit.UnitName);
                        m_Area.value = Cntparea;
                        m_Area.units = outAreaUnit.pUnit;
                        if (outAreaUnit.UnitName == "Unkown Units")
                            AreaUnitMess = outAreaUnit.UnitName;
                        else AreaUnitMess = " Square " + outAreaUnit.UnitName;
                        Cntparea = Math.Round(Cntparea, 6);
                        SumAreas += Cntparea;

                        UnitSumPeri = SumPerimeters;
                        UnitSumArea = SumAreas;
                        txtMessage.Text = "面要素" + "\r\n周长是：" + pFeatPerimeter + outUnit.UnitName + "\r\n面积是：" + Cntparea + AreaUnitMess;
                        if (tbtnSum.Checked == true)
                            txtMessage.Text += "\r\n" + "\r\n周长长度之和是：" + SumPerimeters + outUnit.UnitName + "\r\n面积之和是：" + SumAreas + AreaUnitMess;
                        break;
                }
             
            }

            return pFeat;
        }


        public void frmFeatDown(ref IMap pMap, ref IPoint pPoint,IActiveView pActView)
        {
            if (tbtnFeature.Checked == true)
            {   

                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    ILayer pLayer = pMap.get_Layer(i);

                    if (pLayer.Visible == false)
                        continue;
                    else 
                    {
                        if (pLayer is IGroupLayer || pLayer is ICompositeLayer)
                        {
                          
                            ICompositeLayer pComLayer = (ICompositeLayer)pLayer;
                            int k = pComLayer.Count;
                            for (int j = 0; j < pComLayer.Count; j++)
                            {
                                ILayer SubLayer = pComLayer.get_Layer(j);
                                if (SubLayer is IFeatureLayer)
                                {
                                    if (FeatureMess(SubLayer, pActView, pPoint) != null)
                                    {
                                        k = j;
                                        break;
                                    }
                                }

                            }

                            if (k < pComLayer.Count)
                                break;

                        }

                        else if (pLayer is IFeatureLayer)
                        {
                            if (FeatureMess(pLayer, pActView, pPoint) != null)
                                break;

                        }           
                           
  
                    }
                                                  
                }

            }
 
        }


        public void frmMove(ref IPoint pPoint,ref INewLineFeedback m_FeedbackLine )
        {

            if (tbtnLine.Checked == true)
            {
                IPoint pPnt = pPoint;
                m_CurPoint = pPnt;
                if (m_FeedbackLine != null)
                {
                    m_FeedbackLine.MoveTo(pPnt);

                    double MoveLength = 0.0;
                    IPoint m_LastPoint = pCollection.get_Point(pCollection.PointCount - 1);
                    if (m_LastPoint != null)
                    {
                        SegLength = Math.Sqrt(Math.Pow((m_LastPoint.X - m_CurPoint.X),2) + Math.Pow((m_LastPoint.Y - m_CurPoint.Y),2));
                        MoveLength = TotalLength + SegLength;
                    }

                    SegLength = CntUnit.ConvertUnits(SegLength, inUnit.pUnit, outUnit.pUnit);
                    MoveLength = CntUnit.ConvertUnits(MoveLength, inUnit.pUnit, outUnit.pUnit);
                    //单位变换
                    m_SegLen.value = SegLength;
                    m_SegLen.units = outUnit.pUnit;
                    m_TotLen.value = MoveLength;
                    m_TotLen.units = outUnit.pUnit;

                    SegLength = Math.Round(SegLength, 6);
                    MoveLength = Math.Round(MoveLength, 6);
                    double tempSumLen = SumLengths + MoveLength;
                    UnitSumLen = tempSumLen;

                    txtMessage.Text = "测量线"+"\r\n线段的长度是：" + SegLength +outUnit.UnitName + "\r\n总长度是：" + MoveLength+outUnit .UnitName;
                    if(tbtnSum.Checked==true)
                    txtMessage.Text += "\r\n" + "\r\n线段长度之和是：" + tempSumLen+ outUnit.UnitName;
                }
            }
        }

        public void frmMove(ref IPoint pPoint, ref INewPolygonFeedback m_FeedbackPolygon)
        {
            if (tbtnArea.Checked == true)
            {
                IPoint pPnt = pPoint;
                m_CurPoint = pPnt;

                if (m_FeedbackPolygon != null)
                {
                    m_FeedbackPolygon.MoveTo(pPnt);

                    double MoveArea = 0.0;
                    double MoveLength = 0.0;
                    double tempLegth = 0.0;

                    IPoint LastPoint = pCollection.get_Point(pCollection.PointCount - 1);
                    IPoint CurPoint = m_CurPoint;
                    IPoint FirstPoint = pCollection.get_Point(0);

                    SegAreaLength = Math.Sqrt(Math.Pow((LastPoint.X - CurPoint.X), 2) + Math.Pow((LastPoint.Y - CurPoint.Y), 2));
                    tempLegth = Math.Sqrt(Math.Pow((CurPoint.X - FirstPoint.X), 2) + Math.Pow((CurPoint.Y - FirstPoint.Y), 2));

                    MoveLength = TotalAreaLength + SegAreaLength + tempLegth;
                    double  CntMoveLength = CntUnit.ConvertUnits(MoveLength, inUnit.pUnit, outUnit.pUnit);
                    double CntSegAreaLength = CntUnit.ConvertUnits(SegAreaLength, inUnit.pUnit, outUnit.pUnit);

                    MoveArea = CaculateArea(pPnt);
                    double CntMoveArea; 
                    string AreaUnitMess;

                    CntMoveArea=ConvertToArea( MoveArea, inAreaUnit.UnitName , outAreaUnit.UnitName);

                    if (outAreaUnit.UnitName == "Unkown Units")
                        AreaUnitMess = outAreaUnit.UnitName;
                    else
                        AreaUnitMess = "Square " + outAreaUnit.UnitName;
                    m_SegLen.value = CntSegAreaLength;
                    m_SegLen.units = outUnit.pUnit;
                    m_TotLen.value = CntMoveLength;
                    m_TotLen.units = outUnit.pUnit;
                    m_Area.value = CntMoveArea;
                    m_Area.units = outAreaUnit.pUnit;

                    CntMoveLength = Math.Round(CntMoveLength, 6);
                    CntMoveArea = Math.Round(CntMoveArea, 6);
                    CntSegAreaLength = Math.Round(CntSegAreaLength, 6);
           

                    double tempSumPeri = SumPerimeters + CntMoveLength;
                    double tempSumAreas = SumAreas + CntMoveArea;
                    UnitSumPeri = tempSumPeri;
                    UnitSumArea = tempSumAreas;
                    txtMessage.Text ="测量面" +"\r\n线段长是" + CntSegAreaLength + outUnit.UnitName + "\r\n周长是：" +CntMoveLength + outUnit.UnitName + "\r\n面积是：" + CntMoveArea + AreaUnitMess;
                    if (tbtnSum.Checked == true) 
                    txtMessage.Text += "\r\n" + "\r\n周长长度之和是：" +tempSumPeri+ outUnit.UnitName + "\r\n面积之和是：" + tempSumAreas+ AreaUnitMess;
                }
              
            }
 
        }

        public void  frmDbClick(ref INewLineFeedback m_FeedbackLine,ref IGeometry m_Geometry)
        {
            if (tbtnLine.Checked == true)
            {
                if (m_FeedbackLine != null)
                {
                    pGeometry = m_FeedbackLine.Stop();
                    m_Geometry = pGeometry;
                    m_FeedbackLine = null;
                }
                             
                IPolyline pPolyLine = (IPolyline)pGeometry;              

                SegLength = LastSeg;
                TotalLength = CntUnit.ConvertUnits(pPolyLine.Length, inUnit.pUnit, outUnit.pUnit);
                SegLength = CntUnit.ConvertUnits(SegLength, inUnit.pUnit, outUnit.pUnit);
                m_SegLen.value = SegLength;
                m_SegLen.units = outUnit.pUnit;
                m_TotLen.value = TotalLength;
                m_TotLen.units = outUnit.pUnit;

                SegLength = Math.Round(SegLength, 6);
                TotalLength = Math.Round(TotalLength, 6);
                SumLengths += TotalLength;
                UnitSumLen = SumLengths;
                txtMessage.Clear();
                txtMessage.Text = "测量线"+"\r\n线段的长度是：" + SegLength+outUnit.UnitName + "\r\n总长度是：" + TotalLength+outUnit.UnitName;
                if(tbtnSum.Checked==true)
                txtMessage.Text+="\r\n"+"\r\n线段长度之和是："+ SumLengths+outUnit.UnitName;
               
            }

            //重新初始化
            TotalLength = 0.0;
            SegLength = 0.0;
            TotalAreaLength = 0.0;
            SegAreaLength = 0.0;
            myArea = 0.0;
            Perimeter = 0.0; 
            pCollection = new MultipointClass();          
            pGeometry = null;
 
        }

        public void frmDbClick(ref INewPolygonFeedback m_FeedbackPolygon, ref IGeometry m_Geometry)
        {
            if (tbtnArea.Checked == true)
            {
                if (m_FeedbackPolygon != null)
                {
                    pGeometry = m_FeedbackPolygon.Stop();
                    m_Geometry = pGeometry;
                    m_FeedbackPolygon = null;
                }

                IPolygon pPloygon = (IPolygon)pGeometry;
                Perimeter = pPloygon.Length;
                Perimeter = CntUnit.ConvertUnits(Perimeter, inUnit.pUnit, outUnit.pUnit);
                m_TotLen.value = Perimeter;
                m_TotLen.units = outUnit.pUnit;
                SegAreaLength = LastSegArea;               
                double CntSegAreaLength = CntUnit.ConvertUnits(SegAreaLength, inUnit.pUnit, outUnit.pUnit);
                m_SegLen.value = CntSegAreaLength;
                m_SegLen.units = outUnit.pUnit;
                pArea = (IArea)pPloygon;
                myArea =Math.Abs( pArea.Area);
                double CntmyArea = myArea;
                 CntmyArea=ConvertToArea( CntmyArea, inAreaUnit.UnitName, outAreaUnit.UnitName);
                 m_Area.value = CntmyArea;
                 m_Area.units = outAreaUnit.pUnit;
                string AreaUnitMess;

                if (outAreaUnit.UnitName == "Unkown Units")
                    AreaUnitMess = outAreaUnit.UnitName;
                else AreaUnitMess = "Square " + outAreaUnit.UnitName;

                Perimeter = Math.Round(Perimeter, 6);
                SumPerimeters += Perimeter;

                CntSegAreaLength = Math.Round(CntSegAreaLength, 6);
                CntmyArea = Math.Round(CntmyArea, 6);
                SumAreas += CntmyArea;
                UnitSumArea = SumAreas;
                UnitSumPeri = SumPerimeters;
                txtMessage.Text = "测量面"+"\r\n线段长是" + CntSegAreaLength + outUnit.UnitName + "\r\n周长是：" + Perimeter + outUnit.UnitName + "\r\n面积是：" + CntmyArea + AreaUnitMess;
                if(tbtnSum.Checked==true)
                txtMessage.Text+="\r\n"+"\r\n周长长度之和是："+ SumPerimeters+outUnit.UnitName +"\r\n面积之和是："+SumAreas+AreaUnitMess ;

            }

            TotalLength = 0.0;
            SegLength = 0.0;
            TotalAreaLength = 0.0;
            SegAreaLength = 0.0;
            myArea = 0.0;
            Perimeter = 0.0;
            pCollection = new MultipointClass();
            pGeometry = null;          
 
        }

        private double CaculateArea()
        {           
            double x1, x2, y1, y2;
            double tempArea = 0.0;           
            object missing = Type.Missing;
            IPointCollection tempCollection = new MultipointClass();
            for (int i = 0; i < pCollection.PointCount; i++)
            {
                IPoint dPoint = pCollection.get_Point(i);
                tempCollection.AddPoint(dPoint,ref  missing,ref  missing);
            }
          
            tempCollection.AddPoint(tempCollection.get_Point(0), ref  missing,ref  missing);           
            int Count = tempCollection.PointCount;

            for (int j = 0; j < Count-1; j++)
            {
                x1 =Convert.ToDouble(tempCollection.get_Point(j).X);
                y1 =Convert.ToDouble(tempCollection.get_Point (j).Y);
                x2 =Convert.ToDouble(tempCollection.get_Point(j + 1).X);
                y2 =Convert.ToDouble(tempCollection.get_Point(j + 1).Y);              
                tempArea += (x1 * y2 - x2 * y1);
            }

            tempArea = Math.Abs(tempArea) / 2;
            return tempArea;

        }

        private double CaculateArea(IPoint CurPoint)
        {
            IPointCollection tempCollection = new MultipointClass();

            object missing = Type.Missing;
            for (int i = 0; i < pCollection.PointCount; i++)
            {
                IPoint dPoint = pCollection.get_Point(i);
                tempCollection.AddPoint(dPoint,ref  missing,ref  missing);
            }         
            
            tempCollection.AddPoint(CurPoint, ref missing, ref missing);           
            tempCollection.AddPoint(tempCollection.get_Point(0), ref missing, ref  missing);
            int Count = tempCollection.PointCount;

            double x1, x2, y1, y2;
            double tempArea = 0.0;
            for (int i = 0; i <Count-1; i++)
            {
                x1 =Convert.ToDouble(tempCollection.get_Point(i).X);
                y1 =Convert.ToDouble(tempCollection. get_Point(i).Y);
                x2 =Convert.ToDouble(tempCollection.get_Point(i + 1).X);
                y2 =Convert.ToDouble(tempCollection .get_Point(i + 1).Y);              
                tempArea += (x1 * y2 - x2 * y1);
            }

            tempArea = Math.Abs(tempArea) / 2;         
            return tempArea;

        }
        private double ConvertToArea( double pArea, string oldUnit, string newUnit)
        {                
            double convertarea=pArea;
            if (oldUnit == "Feet")
            {
                if (newUnit == "Kilometers")
                    convertarea = pArea * 0.09290304 / 1000000;
                else if (newUnit == "Meters")
                    convertarea = pArea * 0.09290304;
                else if (newUnit == "Miles")
                    convertarea = pArea * 0.00000003587;
                else if (newUnit == "Yards")
                    convertarea = pArea * 0.1111111;
                else if (newUnit == "Hectares")
                    convertarea = pArea * 0.0000093;
                else if (newUnit == "Acres")
                    convertarea = pArea * 0.000023;
             
                
            }
            else if (oldUnit =="Meters")
            {
                if (newUnit == "Feet")
                    convertarea = pArea * 10.76391042;
                else if (newUnit == "Kilometers")
                    convertarea = pArea * 1.0e-6;
                else if (newUnit == "Miles")
                    convertarea = pArea * 3.8610216e-7;
                else if (newUnit == "Yards")
                    convertarea = pArea * 1.1959900;
                else if (newUnit == "Hectares")
                    convertarea = pArea * 0.0001;
                else if (newUnit == "Acres")
                    convertarea = pArea * 0.0002471;
            

            }
          
            else if (oldUnit == "Miles")
            {
                if (newUnit == "Feet")
                    convertarea = pArea * 27878400;
                else if (newUnit == "Yards")
                    convertarea = pArea * 3097600;
                else if (newUnit == "Kilometers")
                    convertarea = pArea * 2.5899881;
                else if (newUnit == "Meters")
                    convertarea = pArea * 2589988.110336;
                else if (newUnit == "Hectares")
                    convertarea = pArea * 258.9988110;
                else if (newUnit == "Acres")
                    convertarea = pArea * 640;

            
            }
            else if (oldUnit =="Kilometers")
            {
                if (newUnit == "Feet")
                    convertarea = pArea * 10763910.4167097;
                else if (newUnit == "Yards")
                    convertarea = pArea * 1195990.0463011;
                else if (newUnit == "Miles")
                    convertarea = pArea * 0.3861022;
                else if (newUnit == "Meters")
                    convertarea = pArea * 1000000;
                else if (newUnit == "Hectares")
                    convertarea = pArea * 100;
                else if (newUnit == "Acres")
                    convertarea = pArea * 247.1053815;
            }

            return convertarea;

        }


        private void tbtnLine_Click(object sender, EventArgs e)
        {
            if (tbtnLine.Checked == true)
            {
                tbtnArea.Checked =false;
                tbtnFeature.Checked = false;
                CheckedBtn = 1;
            }

        }

        private void tbtnArea_Click(object sender, EventArgs e)
        {
            if (tbtnArea.Checked == true)
            {
                tbtnFeature.Checked = false;
                tbtnLine.Checked = false;
                CheckedBtn = 2;
            }
        }

        private void tbtnFeature_Click(object sender, EventArgs e)
        {
            if (tbtnFeature.Checked == true)
            {
                tbtnLine.Checked = false;
                tbtnArea.Checked = false;
                CheckedBtn = 3;
            }
        }

        private void tbtnSum_Click(object sender, EventArgs e)
        {
            if (tbtnSum.Checked == true)
            {
                SumLengths = 0.0;
                SumAreas = 0.0;
                SumPerimeters = 0.0;
            }
        }

        private void tbtnClear_Click(object sender, EventArgs e)
        {
            
            txtMessage.Clear();
            SumAreas = 0.0;
            SumLengths = 0.0;
            SumPerimeters = 0.0;
        }

        private void txtMessChange()
        {
            if (CheckedBtn == 1)
            {
                double se_length = CntUnit.ConvertUnits(m_SegLen.value, m_SegLen.units, outUnit.pUnit);
                double to_length = CntUnit.ConvertUnits(m_TotLen.value, m_TotLen.units, outUnit.pUnit);
                double sum_len = CntUnit.ConvertUnits(UnitSumLen, m_TotLen.units, outUnit.pUnit);
                se_length = Math.Round(se_length, 6);
                to_length = Math.Round(to_length, 6);
                sum_len = Math.Round(sum_len, 6);
                txtMessage.Text ="测量线"+ "\r\n线段的长度是：" + se_length + outUnit.UnitName + "\r\n总长度是：" + to_length + outUnit.UnitName;
                if (tbtnSum.Checked == true)
                    txtMessage.Text += "\r\n" + "\r\n线段长度之和是：" + sum_len + outUnit.UnitName;
            }

            else if (CheckedBtn == 2)
            {

                double segarea = CntUnit.ConvertUnits(m_SegLen.value, m_SegLen.units, outUnit.pUnit);
                double areperi = CntUnit.ConvertUnits(m_TotLen.value, m_TotLen.units, outUnit.pUnit);
                double mianji = ConvertToArea(m_Area.value, m_Area.units.ToString().Remove(0, 4), outAreaUnit.UnitName);
                double sum_peri = CntUnit.ConvertUnits(UnitSumPeri, m_TotLen.units, outUnit.pUnit);
                double sum_Area = ConvertToArea(UnitSumArea, m_Area.units.ToString().Remove(0, 4), outAreaUnit.UnitName);
                mianji = Math.Round(mianji, 6);
                segarea = Math.Round(segarea, 6);
                areperi = Math.Round(areperi, 6);
                sum_peri = Math.Round(sum_peri, 6);
                sum_Area = Math.Round(sum_Area, 6);
                txtMessage.Text = "测量面"+"\r\n线段长是" + segarea + outUnit.UnitName + "\r\n周长是：" + areperi + outUnit.UnitName + "\r\n面积是：" + mianji + " Square " + outAreaUnit.UnitName;
                if (tbtnSum.Checked == true)
                    txtMessage.Text += "\r\n" + "\r\n周长长度之和是：" + sum_peri+outUnit.UnitName  + "\r\n面积之和是：" + sum_Area +"Square" +outAreaUnit.UnitName;

            }

            else if (CheckedBtn == 3)
            {
                if (shapeType == 1)
                {
                    double p_x = CntUnit.ConvertUnits(m_SegLen.value, m_SegLen.units, outUnit.pUnit);
                    double p_y = CntUnit.ConvertUnits(m_TotLen.value, m_TotLen.units, outUnit.pUnit);
                    p_x = Math.Round(p_x, 6);
                    p_y = Math.Round(p_y, 6);
                    txtMessage.Text = "点要素" + "\r\nX:" +p_x + outUnit.UnitName + "\r\nY:" +p_y+ outUnit.UnitName;
                }
                else if (shapeType == 2)
                {
                    double to_length = CntUnit.ConvertUnits(m_TotLen.value, m_TotLen.units, outUnit.pUnit);
                    double sum_len=CntUnit.ConvertUnits(UnitSumLen,m_TotLen.units,outUnit.pUnit );
                    to_length = Math.Round(to_length, 6);
                    sum_len=Math.Round(sum_len,6);

                    txtMessage.Text = "线要素" + "\r\n线段长度是：" + to_length+ outUnit.UnitName;
                    if(tbtnSum.Checked==true)
                        txtMessage.Text+="\r\n"+"\r\n线段长度之和是："+sum_len+outUnit.UnitName ;

                }

                else if (shapeType == 3)
                {                   
                    double to_length = CntUnit.ConvertUnits(m_TotLen.value, m_TotLen.units, outUnit.pUnit);
                    double p_area = ConvertToArea(m_Area.value, m_Area.units.ToString().Remove(0, 4), outAreaUnit.UnitName);
                   double sum_peri=CntUnit.ConvertUnits(UnitSumPeri ,m_TotLen.units,outUnit.pUnit);
                    double sum_area=ConvertToArea(UnitSumArea,m_Area.units.ToString().Remove(0,4),outAreaUnit.UnitName);
                    sum_peri=Math.Round(sum_peri,6);
                    sum_area=Math.Round(sum_area,6);
                    to_length = Math.Round(to_length, 6);
                    p_area = Math.Round(p_area, 6);
                    txtMessage.Text = "面要素" + "\r\n周长是：" + to_length + outUnit.UnitName + "\r\n面积是：" + p_area + " Square " + outAreaUnit.UnitName;
                    if (tbtnSum.Checked==true )
                    txtMessage.Text+="\r\n"+"\r\n周长长度之和是："+sum_peri+outUnit.UnitName+"\r\n面积之和是："+sum_area+"Square"+outAreaUnit.UnitName ;
                }

            }
            
        }

        private void kiloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kiloToolStripMenuItem.Checked == true)
            {

                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;
                kiloToolStripMenuItem.Checked = true;               
                outUnit.pUnit = esriUnits.esriKilometers;
                outUnit.UnitName = "Kilometers";
                txtMessChange();
               
            }
        }
       
        private void metersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (metersToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;

                metersToolStripMenuItem.Checked = true;               

                outUnit.pUnit = esriUnits.esriMeters;
                outUnit.UnitName = "Meters";
                if (txtMessage.Text!="")
                txtMessChange();

            }
        }

        private void decimetersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decimetersToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;

                decimetersToolStripMenuItem.Checked = true;
               
                outUnit.pUnit = esriUnits.esriDecimeters;
                outUnit.UnitName = "Decimeters";
                if (txtMessage.Text!="")
                txtMessChange();
            }
        }

        private void centimetersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (centimetersToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;

                centimetersToolStripMenuItem.Checked = true;

                outUnit.pUnit = esriUnits.esriCentimeters;
                outUnit.UnitName = "Centimeters";
                if (txtMessage.Text != "")
                txtMessChange();

            }

        }

        private void millimetersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (millimetersToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;

                millimetersToolStripMenuItem.Checked = true;
              
                outUnit.pUnit = esriUnits.esriMillimeters;
                outUnit.UnitName = "Millimeters";
                if (txtMessage.Text != "")
                txtMessChange();
            }
        }

        private void milesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (milesToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;

                milesToolStripMenuItem.Checked = true;
                               
                outUnit.pUnit = esriUnits.esriMiles;
                outUnit.UnitName = "Miles";
                if (txtMessage.Text != "")
                txtMessChange();

            }
        }

        private void nautToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nautToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;
                nautToolStripMenuItem.Checked = true;
               
                outUnit.pUnit = esriUnits.esriNauticalMiles;
                outUnit.UnitName = "NauticalMiles";
                if (txtMessage.Text != "")
                txtMessChange();
 
            }
        }

        private void yardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (yardsToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;
                yardsToolStripMenuItem.Checked = true;

                outUnit.pUnit = esriUnits.esriYards;
                outUnit.UnitName = "Yards";
                if (txtMessage.Text != "")
                txtMessChange();

            }
        }

        private void feetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (feetToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;
                feetToolStripMenuItem.Checked = true;               

                outUnit.pUnit = esriUnits.esriFeet;
                outUnit.UnitName = "Feet";
                if (txtMessage.Text != "")
                txtMessChange();

            }
        }

        private void inchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inchesToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;
                inchesToolStripMenuItem.Checked = true;
              
                outUnit.pUnit = esriUnits.esriInches;
                outUnit.UnitName = "Inches";
                if (txtMessage.Text != "")
                txtMessChange();

            }

        }

        private void decimalDegreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decimalDegreeToolStripMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in DistanceToolItem.DropDownItems)
                    tool.Checked = false;
                decimalDegreeToolStripMenuItem.Checked = true;               

                outUnit.pUnit = esriUnits.esriDecimalDegrees;
                outUnit.UnitName = "DecimalDegrees";
                if (txtMessage.Text != "")
                txtMessChange();
            }
        }

        private void kiloAreaTSMenuItem_Click(object sender, EventArgs e)
        {
            if (kiloAreaTSMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                    tool.Checked = false;
                kiloAreaTSMenuItem.Checked = true;

                outAreaUnit.pUnit = esriUnits.esriKilometers;
                outAreaUnit.UnitName = "Kilometers";

                if (txtMessage.Text != "")
                    txtMessChange();
            }
        }

        private void metersAreaTSMenuItem_Click(object sender, EventArgs e)
        {
            if (metersAreaTSMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                    tool.Checked = false;
                metersAreaTSMenuItem.Checked = true;

                outAreaUnit.pUnit = esriUnits.esriMeters;
                outAreaUnit.UnitName = "Meters";

                if (txtMessage.Text != "")
                    txtMessChange();
 
            }
        }

        private void milesAreaTSMenuItem_Click(object sender, EventArgs e)
        {
            if (milesAreaTSMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                    tool.Checked = false;
                milesAreaTSMenuItem.Checked = true;

                outAreaUnit.pUnit = esriUnits.esriMiles;
                outAreaUnit.UnitName = "Miles";

                if (txtMessage.Text != "")
                    txtMessChange();
            }
        }

        private void feetAreaTSMenuItem_Click(object sender, EventArgs e)
        {
            if (feetAreaTSMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                    tool.Checked = false;
                feetAreaTSMenuItem.Checked = true;

                outAreaUnit.pUnit = esriUnits.esriFeet;
                outAreaUnit.UnitName = "Feet";

                if (txtMessage.Text != "")
                    txtMessChange();
            }
        }

        private void yardsAreaTSMenuItem_Click(object sender, EventArgs e)
        {
            if (yardsAreaTSMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                    tool.Checked = false;
                yardsAreaTSMenuItem.Checked = true;

                outAreaUnit.pUnit = esriUnits.esriYards;
                outAreaUnit.UnitName = "Yards";

                if (txtMessage.Text != "")
                    txtMessChange();
            }
        }

        private void hectaresTSMenuItem_Click(object sender, EventArgs e)
        {
            if (hectaresTSMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                    tool.Checked = false;
                hectaresTSMenuItem.Checked = true;

                outAreaUnit.pUnit  = esriUnits.esriUnknownUnits;
                outAreaUnit.UnitName = "Hectares";

                if (txtMessage.Text != "")
                    txtMessChange();
            }
        }

        private void acresTSMenuItem_Click(object sender, EventArgs e)
        {
            if (acresTSMenuItem.Checked == true)
            {
                foreach (ToolStripMenuItem tool in AreaToolItem.DropDownItems)
                    tool.Checked = false;
                acresTSMenuItem.Checked = true;

                outAreaUnit.pUnit = esriUnits.esriUnknownUnits;
                outAreaUnit.UnitName = "Acres";

                if (txtMessage.Text != "")
                    txtMessChange();
            }
        }       
     
       
    }
}