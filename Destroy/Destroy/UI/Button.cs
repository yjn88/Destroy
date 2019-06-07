﻿namespace Destroy.UI
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

        /// <summary>
        /// 图形容器
        /// </summary>
        public GraphicContainer GraphicContainer;

        /// <summary>
        /// 是否触摸直接生效(一般来说设置该值为true可以增强灵敏度)
        /// </summary>
        public bool Touch;

        private UIManager manager;

        private HashSet<Vector2> positions;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="manager">UI管理器</param>
        /// <param name="graphicGrid">图形网格</param>
        public Button(UIManager manager, GraphicGrid graphicGrid)
        {
            this.manager = manager;
            manager.AddUIObject(this);
            positions = new HashSet<Vector2>();
            positions.Add(graphicGrid.Position);
            GraphicContainer = new GraphicContainer(graphicGrid);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="manager">UI管理器</param>
        /// <param name="graphicGrids">图形网格集合</param>
        public Button(UIManager manager, List<GraphicGrid> graphicGrids)
        {
            this.manager = manager;
            manager.AddUIObject(this);
            positions = new HashSet<Vector2>();
            foreach (GraphicGrid item in graphicGrids)
            {
                positions.Add(item.Position);
            }
            GraphicContainer = new GraphicContainer(graphicGrids);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public override void Update()
        {
            Vector2 pos = Input.MousePositionInConsole;
            Vector2 cursorPos = new Vector2(pos.X / (int)manager.Graphics.CharWidth, pos.Y);

            bool enter = positions.Contains(cursorPos) && Input.MouseInConsole;

            if (enter)
            {
                OnEnter?.Invoke();
                if (Touch)
                {
                    if (Input.GetMouseButton(MouseButton.Left))
                    {
                        OnClick?.Invoke();
                    }
                }
                else
                {
                    if (Input.GetMouseButtonUp(MouseButton.Left))
                    {
                        OnClick?.Invoke();
                    }
                }
            }
            else
            {
                OnLeave?.Invoke();
            }
        }
    }
}