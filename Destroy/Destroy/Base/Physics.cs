namespace Destroy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 碰撞器
    /// </summary>
    public class Collider
    {
        /// <summary>
        /// 当碰撞体进入时回调
        /// </summary>
        public Action<Collision> OnCollisionEnter;

        /// <summary>
        /// 碰撞点
        /// </summary>
        public List<Vector2> Points;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="point">碰撞点</param>
        public Collider(Vector2 point)
        {
            Points = new List<Vector2> { point };
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="points">碰撞点</param>
        public Collider(List<Vector2> points)
        {
            Points = points;
        }

        /// <summary>
        /// 增加碰撞点的坐标
        /// </summary>
        /// <param name="position">坐标</param>
        public void AddPosition(Vector2 position)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i] += position;
            }
        }

        /// <summary>
        /// 设置碰撞点的坐标
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="offset">偏移量</param>
        public void SetPosition(Vector2 position, Vector2 offset = default(Vector2))
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i] = position;
                position += offset;
            }
        }
    }

    /// <summary>
    /// 碰撞体
    /// </summary>
    public class Collision
    {
        /// <summary>
        /// 发生碰撞的坐标
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// 发生碰撞的碰撞体
        /// </summary>
        public List<Collider> OtherColliders;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="otherColliders">其他碰撞体</param>
        public Collision(Vector2 position, List<Collider> otherColliders)
        {
            Position = position;
            OtherColliders = otherColliders;
        }
    }

    /// <summary>
    /// 物理系统
    /// </summary>
    public class Physics
    {
        /// <summary>
        /// 坐标与碰撞器字典
        /// </summary>
        public Dictionary<Vector2, List<Collider>> Colliders;

        /// <summary>
        /// 构造方法
        /// </summary>
        public Physics()
        {
            Colliders = new Dictionary<Vector2, List<Collider>>();
        }

        /// <summary>
        /// 添加碰撞器(该方法可能会触发碰撞回调)
        /// </summary>
        /// <param name="collider">碰撞器</param>
        public void AddCollider(Collider collider)
        {
            HashSet<Collider> crashSet = new HashSet<Collider>();
            crashSet.Add(collider); //确保不会与自己发生碰撞

            foreach (Vector2 point in collider.Points)
            {
                //该点存在其他碰撞器
                if (Colliders.ContainsKey(point))
                {
                    List<Collider> otherColliders = new List<Collider>();
                    foreach (Collider other in Colliders[point])
                    {
                        //保证自己不会与另一个碰撞体碰撞两次
                        if (!crashSet.Contains(other))
                        {
                            crashSet.Add(other);
                            otherColliders.Add(other);
                        }
                    }
                    //发生了碰撞
                    if (otherColliders.Count > 0)
                    {
                        Collision collision = new Collision(point, otherColliders);
                        collider.OnCollisionEnter?.Invoke(collision);
                    }
                    //添加:保证自己不会被同一个点添加多次
                    if (!Colliders[point].Contains(collider))
                    {
                        Colliders[point].Add(collider);
                    }
                }
                else
                {
                    //直接添加
                    Colliders.Add(point, new List<Collider> { collider });
                }
            }
        }

        /// <summary>
        /// 移除碰撞器
        /// </summary>
        /// <param name="collider">碰撞器</param>
        public void RemoveCollider(Collider collider)
        {
            foreach (Vector2 point in collider.Points)
            {
                if (Colliders.ContainsKey(point))
                {
                    Colliders[point].Remove(collider);
                    //如果该点没有集合了则清除该点的集合
                    if (Colliders[point].Count == 0)
                    {
                        Colliders.Remove(point);
                    }
                }
            }
        }

        /// <summary>
        /// 判断该碰撞器是否位于该坐标点
        /// </summary>
        /// <param name="collider">碰撞器</param>
        /// <param name="position">坐标</param>
        /// <returns>是否位于</returns>
        public bool ContainsCollider(Collider collider, Vector2 position)
        {
            if (Colliders.ContainsKey(position))
            {
                bool r = Colliders[position].Contains(collider);
                return r;
            }
            return false;
        }

        /// <summary>
        /// 尝试移动该碰撞器(该方法并不会触发碰撞回调)
        /// </summary>
        /// <param name="collider">碰撞器</param>
        /// <param name="addition">增量</param>
        /// <returns>是否发生碰撞</returns>
        public bool TryToMove(Collider collider, Vector2 addition)
        {
            bool allow = true;
            foreach (Vector2 point in collider.Points)
            {
                Vector2 pos = point + addition;
                if (Colliders.ContainsKey(pos))
                {
                    allow = false;
                }
            }
            return allow;
        }

        /// <summary>
        /// 移动该碰撞器(该方法可能会触发碰撞回调)
        /// </summary>
        /// <param name="collider">碰撞器</param>
        /// <param name="addition">增量</param>
        public void Move(Collider collider, Vector2 addition)
        {
            RemoveCollider(collider);
            collider.AddPosition(addition);
            AddCollider(collider);
        }
    }
}