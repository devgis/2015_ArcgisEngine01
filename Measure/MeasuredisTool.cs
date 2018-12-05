using System;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto ;
using ESRI.ArcGIS.Geometry ;
using ESRI.ArcGIS.Display ;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;


namespace Measure
{
    /// <summary>
    /// Summary description for MeasuredisTool.
    /// </summary>
    [Guid("7402a3c8-504f-42a6-b7d1-595a6273337f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Measure.MeasuredisTool")]
    public sealed class MeasuredisTool : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion
        private System.Windows.Forms.Cursor m_Cursor;
        private IGeometry m_Geometry;      
        private INewLineFeedback m_FeedbackLine;
        private INewPolygonFeedback m_FeedbackPolygon;
        private IHookHelper m_hookHelper = null;
        private static FrmMeasure frm;
        private bool right = true;
        private bool left = true;
        private bool top = true;
        private bool bottom = true;

        public MeasuredisTool()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "";  //localizable text 
            base.m_message = "This should work in ArcMap/MapControl/PageLayoutControl";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            //frm = new FrmMeasure(m_hookHelper);
            m_Cursor = System.Windows.Forms.Cursors.Cross;            
            
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add other initialization code
        }
       

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add MeasuredisTool.OnClick implementation   
            frm = GetFrm();
            frm.GetMapUnit(m_hookHelper.FocusMap);        
               frm.Show();
               frm.TopMost = true;         
        }

        private FrmMeasure GetFrm()
        {
            if (frm == null || frm.IsDisposed)
                frm = new FrmMeasure(m_hookHelper);
            return frm;
 
        }
           
     
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add MeasuredisTool.OnMouseDown implementation
            frm = GetFrm();
            frm.Show();
            frm.TopMost = true;
           

            IPoint pPnt;            
            IMap pMap = m_hookHelper.FocusMap;            
            IActiveView pActView = m_hookHelper.ActiveView;
            
            if (Button == 1)
            {

               pPnt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
           
                if (frm.CheckedBtn == 1)
                {
                    if (m_FeedbackLine == null)
                    {
                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                        m_FeedbackLine = new NewLineFeedbackClass();
                        m_FeedbackLine.Display = m_hookHelper.ActiveView.ScreenDisplay;
                        m_FeedbackLine.Start(pPnt);

                    }
                    else
                        m_FeedbackLine.AddPoint(pPnt);

                    frm.frmLineDown(ref pPnt);
                }

                if (frm.CheckedBtn == 2)
                {
                    if (m_FeedbackPolygon == null)
                    {
                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                        m_FeedbackPolygon = new NewPolygonFeedbackClass();
                        m_FeedbackPolygon.Display = m_hookHelper.ActiveView.ScreenDisplay;
                        m_FeedbackPolygon.Start(pPnt);
                    }
                    else
                        m_FeedbackPolygon.AddPoint(pPnt);

                    frm.frmAreaDown(ref pPnt);
                }

                if(frm.CheckedBtn==3)
                 frm.frmFeatDown(ref pMap, ref pPnt,pActView);

            }
                  
        }


        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add MeasuredisTool.OnMouseMove implementation
            IPoint pPnt;
            pPnt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            //移动图片
            IEnvelope MEnvelop = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds;
            IEnvelope FullEnv = m_hookHelper.ActiveView.FullExtent;
            

            if (m_FeedbackLine!=null || m_FeedbackPolygon !=null)
            {
                if (MEnvelop.XMax - pPnt.X > MEnvelop.Width /50)
                    right = true;
                if (pPnt.X - MEnvelop.XMin > MEnvelop.Width / 50)
                    left = true;
                if (MEnvelop.YMax- pPnt.Y > MEnvelop.Height / 50)
                    top = true;
                if (pPnt.Y - MEnvelop.YMin > MEnvelop.Height / 50)
                    bottom = true;

                if (MEnvelop.XMax-pPnt.X <MEnvelop.Width/100)
                {
                    double vWidth = MEnvelop.Width;
                    IEnvelope MoveEnv = new EnvelopeClass();
                    MoveEnv.XMin = MEnvelop.XMin + vWidth / 2;
                    MoveEnv.XMax = MEnvelop.XMax + vWidth / 2;                  
                    MoveEnv.YMax = MEnvelop.YMax;
                    MoveEnv.YMin = MEnvelop.YMin;
                    if (MoveEnv.XMax >= FullEnv.XMax)
                    {
                        MoveEnv.XMin = MoveEnv.XMin - (MoveEnv.XMax - FullEnv.XMax);
                        MoveEnv.XMax = FullEnv.XMax;

                    }
                    if (right)
                    {
                        m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds = MoveEnv;
                        right = false;
                    }
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                }
                if ( pPnt.X-MEnvelop.XMin<MEnvelop.Width/100)
                {
                    double vWidth = MEnvelop.Width;

                    IEnvelope MoveEnv = new EnvelopeClass();
                    MoveEnv.XMin = MEnvelop.XMin - vWidth / 2;
                    MoveEnv.XMax = MEnvelop.XMax - vWidth / 2;
                    MoveEnv.YMax = MEnvelop.YMax;
                    MoveEnv.YMin = MEnvelop.YMin;
                    if (MoveEnv.XMin <= FullEnv.XMin)
                    {
                        MoveEnv.XMax = MoveEnv.XMax + (FullEnv.XMin - MoveEnv.XMin);
                        MoveEnv.XMin = FullEnv.XMin;
                    }
                    if (left)
                    {
                        m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds = MoveEnv;
                        left = false;
                    }
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);


                }
                if (MEnvelop.YMax -pPnt.Y <MEnvelop.Height/100)
                {
                    double vHeight = MEnvelop.Height;
                    IEnvelope MoveEnv = new EnvelopeClass();
                    MoveEnv.XMin = MEnvelop.XMin;
                    MoveEnv.XMax = MEnvelop.XMax;
                    MoveEnv.YMax = MEnvelop.YMax + vHeight / 2;
                    MoveEnv.YMin = MEnvelop.YMin + vHeight / 2;
                    if (MoveEnv.YMax >= FullEnv.YMax)
                    {
                        MoveEnv.YMin = MoveEnv.YMin - (MoveEnv.YMax - FullEnv.YMax);
                        MoveEnv.YMax = FullEnv.YMax;
                    }
                    if (top)
                    {
                        top = false;
                        m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds = MoveEnv;
                    }
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);


                }
                if (pPnt.Y -MEnvelop.YMin<MEnvelop.Height/100)
                {
                    double vHeight = MEnvelop.Height;
                    IEnvelope MoveEnv = new EnvelopeClass();
                    MoveEnv.XMin = MEnvelop.XMin;
                    MoveEnv.XMax = MEnvelop.XMax;
                    MoveEnv.YMax = MEnvelop.YMax - vHeight / 2;
                    MoveEnv.YMin = MEnvelop.YMin - vHeight / 2;
                    if (MoveEnv.YMin <= FullEnv.YMin)
                    {
                        MoveEnv.YMax = MoveEnv.YMax + (FullEnv.YMin - MoveEnv.YMin);
                        MoveEnv.YMin = FullEnv.YMin;
                    }
                    if (bottom)
                    {
                        m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds = MoveEnv;
                        bottom = false;
                    }
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);


                }
            }


            frm.frmMove(ref pPnt,ref m_FeedbackLine);
            frm.frmMove(ref pPnt, ref m_FeedbackPolygon);
           
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add MeasuredisTool.OnMouseUp implementation
        }

        public override void Refresh(int hDC)
        {
            if (m_FeedbackLine != null)
                m_FeedbackLine.Refresh(hDC);

            if (m_FeedbackPolygon != null)
                m_FeedbackPolygon.Refresh(hDC);
         
        }
        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (keyCode == (int)Keys.Escape)
            {
                if (m_FeedbackLine != null)
                {
                    m_FeedbackLine.Stop();
                    m_FeedbackLine = null;
                }

                if (m_FeedbackPolygon != null)
                {
                    m_FeedbackPolygon.Stop();
                    m_FeedbackPolygon = null;
                }
                m_Geometry = null;
            }
         
        }
             

        public override int Cursor
        {
            get
            {
                return m_Cursor.Handle.ToInt32();
            }
        }
        public override bool Deactivate()
        {
            return true;
        }


        public override void OnDblClick()
        {
            frm.frmDbClick(ref m_FeedbackLine,ref m_Geometry);
            frm.frmDbClick(ref m_FeedbackPolygon, ref m_Geometry);
           
            //base.OnDblClick();
        }
       


        #endregion
    }
}
