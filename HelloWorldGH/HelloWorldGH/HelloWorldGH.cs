/**
Copyright (c) 2018 hiron_rgrk
This software is released under the MIT License.
See LICENSE
**/

using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;

using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// コンポーネントの定義
/// </summary>
namespace HelloWorldGH
{
    public class HelloWorldGHComponent : GH_Component
    {
        public HelloWorldGHComponent() 
            : base("Hello World!", 
                   "Hello World!", 
                   "HeloWorld component showing winforms override",
                   "rgkr", 
                   "HelloWorld"
                  )
        {
        }

        /// <summary>
        /// カスタムの設定
        /// </summary>
        public override void CreateAttributes()
        {
            m_attributes = new Attributes_Custom(this);
        }

        /// <summary>
        /// インプットパラメータの登録
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Flag", "F", "parameter", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// アウトプットパラメータの登録
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Output", "O", "Output value", GH_ParamAccess.item);
        }

        /// <summary>
        /// 解析を実行する箇所
        /// </summary>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool option = true;
            if (!DA.GetData(0, ref option)) return;

            switch (option)
            {
                case true:
                    DA.SetData(0, "HelloWorld!");
                    return;
                case false:
                    DA.SetData(0, "false");
                    return;
            }
        }

        /// <summary>
        /// GUIDの設定
        /// </summary>
        public override System.Guid ComponentGuid
        {
            get { return new Guid("{DE314B90-AA73-4793-AB1C-051659B486E9}"); }
        } 
        
        /// <summary>
        /// アイコンの設定。24x24 pixelsが推奨
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return HelloWorldGH.Properties.Resource.icon;
            }
        }
    }

    /// <summary>
    /// コンポーネントのカスタムの中身のクラス
    /// </summary>
    public class Attributes_Custom : GH_ComponentAttributes
    {
        public Attributes_Custom(GH_Component owner) : base(owner) { }
        protected override void Layout()
        {
            base.Layout();

            Rectangle rec0 = GH_Convert.ToRectangle(Bounds);
            rec0.Height += 44;

            Rectangle rec1 = rec0;
            rec1.Y = rec1.Bottom - 44;
            rec1.Height = 22;
            rec1.Inflate(-2, -2);

            Rectangle rec2 = rec0;
            rec2.Y = rec0.Bottom - 22;
            rec2.Height = 22;
            rec2.Inflate(-2, -2);

            Bounds = rec0;
            ButtonBounds = rec1;
            ButtonBounds2 = rec2;
        }
        private Rectangle ButtonBounds { get; set; }
        private Rectangle ButtonBounds2 { get; set; }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);
            if (channel == GH_CanvasChannel.Objects)
            {
                GH_Capsule button = GH_Capsule.CreateTextCapsule(ButtonBounds, ButtonBounds, GH_Palette.Black, "Button1", 2, 0);
                button.Render(graphics, Selected, Owner.Locked, false);
                button.Dispose();
            }
            if (channel == GH_CanvasChannel.Objects)
            {
                GH_Capsule button2 = GH_Capsule.CreateTextCapsule(ButtonBounds2, ButtonBounds2, GH_Palette.Black, "Button2", 2, 0);
                button2.Render(graphics, Selected, Owner.Locked, false);
                button2.Dispose();
            }
        }

        /// <summary>
        /// マウスをクリックしたときのイベントハンドラ
        /// </summary>
        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            // ButtonBoundsを押したときのイベント
            if (e.Button == MouseButtons.Left)
            {
                RectangleF rec = ButtonBounds;
                if (rec.Contains(e.CanvasLocation))
                {
                    MessageBox.Show("Hello World", "Hello World", MessageBoxButtons.OK);
                    return GH_ObjectResponse.Handled;
                }
            }

            // ButtonBounds2を押したときのイベント
            if (e.Button == MouseButtons.Left)
            {
                RectangleF rec = ButtonBounds2;
                if (rec.Contains(e.CanvasLocation))
                {
                    MessageBox.Show("こんな感じで増えます。", "増やし方", MessageBoxButtons.OK);
                    return GH_ObjectResponse.Handled;
                }
            }
            return base.RespondToMouseDown(sender, e);
        }
    }
}