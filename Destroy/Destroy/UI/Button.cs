namespace Destroy.UI
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 按钮
    /// </summary>
    public class Button : UIObject
    {
        /// <summary>
        /// 鼠标进入事件
        /// </summary>
        public event Action OnEnter;

        /// <summary>
        /// 鼠标离开事件
        /// </summary>
        public event Action OnLeave;

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        public event Action OnClick;

        private List<GraphicGrid> graphicGrids;

        private CharWidth charWidth;

        private HashSet<Vector2> positions;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="graphicGrids">图形网格集合</param>
        /// <param name="charWidth">字符宽度</param>
        public Button(List<GraphicGrid> graphicGrids, CharWidth charWidth)
        {
            this.graphicGrids = graphicGrids;
            this.charWidth = charWidth;
            positions = new HashSet<Vector2>();
            foreach (GraphicGrid item in graphicGrids)
            {
                positions.Add(item.Position);
            }
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public override void Update()
        {
            Vector2 cursorPos = new Vector2(Input.CursorPosition.X / (int)charWidth, Input.CursorPosition.Y);

            bool enter = positions.Contains(cursorPos) && Input.MouseInConsole;

            if (enter)
            {
                OnEnter?.Invoke();
                if (Input.GetMouseButtonUp(MouseButton.Left))
                {
                    OnClick?.Invoke();
                }
            }
            else
            {
                OnLeave?.Invoke();
            }
        }
    }
}