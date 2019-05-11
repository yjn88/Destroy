namespace Destroy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 搜索结果
    /// </summary>
    public struct SearchResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success;

        /// <summary>
        /// 搜索的完整路径点
        /// </summary>
        public List<Vector2> Nodes;

        /// <summary>
        /// 所有搜索过的点与该点距离终点的距离(可以用于评估算法的效率)
        /// </summary>
        public Dictionary<Vector2, int> Dict;
    }

    /// <summary>
    /// 提供寻路算法支持 <see langword="static"/>
    /// </summary>
    public static class Navigation
    {
        /// <summary>
        /// 使用BFS算法进行搜索
        /// </summary>
        /// <param name="start">开始点</param>
        /// <param name="end">结束点</param>
        /// <param name="func">指示该坐标是否可以通过</param>
        /// <returns>搜索结果</returns>
        public static SearchResult BFS(Vector2 start, Vector2 end, Func<Vector2, bool> func)
        {
            //搜索结果
            SearchResult result = new SearchResult();
            //验证终点是否可以搜索
            if (func(end) == false)
            {
                return result;
            }

            //已搜索字典
            Dictionary<Vector2, int> dict = new Dictionary<Vector2, int>();
            dict.Add(start, 0);
            //待搜索队列
            Queue<Vector2> queue = new Queue<Vector2>();
            queue.Enqueue(start);

            //搜索方法
            bool SearchNext(Vector2 pos, Vector2 dir)
            {
                Vector2 next = pos + dir;
                //该坐标未被搜索且可以搜索
                if (func(next) && !dict.ContainsKey(next))
                {
                    //加入已搜索字典
                    dict.Add(next, dict[pos] + 1);
                    //搜索成功
                    if (next == end)
                    {
                        result.Success = true;
                        return true;
                    }
                    //加入待搜索列表
                    else
                    {
                        queue.Enqueue(next);
                    }
                }
                return false;
            }

            while (queue.Count > 0)
            {
                Vector2 node = queue.Dequeue();
                //查找4个方向
                if (SearchNext(node, new Vector2(0, 1)) ||
                    SearchNext(node, new Vector2(0, -1)) ||
                    SearchNext(node, new Vector2(-1, 0)) ||
                    SearchNext(node, new Vector2(1, 0)))
                {
                    break;
                }
            }

            //计算最优路径
            result.Nodes = GetPath(start, end, dict);
            result.Dict = dict;

            return result;
        }

        /// <summary>
        /// 使用距离指导的DFS算法进行搜索
        /// </summary>
        /// <param name="start">开始点</param>
        /// <param name="end">结束点</param>
        /// <param name="func">指示该坐标是否可以通过</param>
        /// <returns>搜索结果</returns>
        public static SearchResult DFS(Vector2 start, Vector2 end, Func<Vector2, bool> func)
        {
            //搜索结果
            SearchResult result = new SearchResult();
            //验证终点是否可以搜索
            if (func(end) == false)
            {
                return result;
            }

            //已搜索字典
            Dictionary<Vector2, int> dict = new Dictionary<Vector2, int>();
            dict.Add(start, 0);
            //待搜索列表
            List<Vector2> list = new List<Vector2>();
            list.Add(start);

            //搜索方法
            bool SearchNext(Vector2 pos, Vector2 dir)
            {
                Vector2 next = pos + dir;
                //该坐标未被搜索且可以搜索
                if (func(next) && !dict.ContainsKey(next))
                {
                    //加入已搜索字典
                    dict.Add(next, dict[pos] + 1);
                    //搜索成功
                    if (next == end)
                    {
                        result.Success = true;
                        return true;
                    }
                    //距离指导
                    else if (next.Distance(end) < pos.Distance(end))
                    {
                        list.Insert(0, next);
                    }
                    //加入待搜索列表
                    else
                    {
                        list.Add(next);
                    }
                }
                return false;
            }

            while (list.Count > 0)
            {
                Vector2 node = list[0];
                //查找4个方向
                if (SearchNext(node, new Vector2(0, 1)) ||
                    SearchNext(node, new Vector2(0, -1)) ||
                    SearchNext(node, new Vector2(-1, 0)) ||
                    SearchNext(node, new Vector2(1, 0)))
                {
                    break;
                }
                list.Remove(node);
            }

            //计算最优路径
            result.Nodes = GetPath(start, end, dict);
            result.Dict = dict;

            return result;
        }

        /// <summary>
        /// 使用BFS算法搜索指定距离内所有可以通过的点
        /// </summary>
        /// <param name="start">开始点</param>
        /// <param name="distance">距离</param>
        /// <param name="func">指示该坐标是否可以通过</param>
        /// <returns>点集合</returns>
        public static List<Vector2> BFSByDistance(Vector2 start, int distance, Func<Vector2, bool> func)
        {
            //结果
            List<Vector2> positions = new List<Vector2>();
            positions.Add(start);
            //已搜索字典
            Dictionary<Vector2, int> dict = new Dictionary<Vector2, int>();
            dict.Add(start, 0);
            //待搜索队列
            Queue<Vector2> queue = new Queue<Vector2>();
            queue.Enqueue(start);

            //搜索方法
            bool SearchNext(Vector2 pos, Vector2 dir)
            {
                Vector2 next = pos + dir;
                if (func(next) && !dict.ContainsKey(next))
                {
                    //超出探索距离
                    if (dict[pos] + 1 > distance)
                    {
                        return true;
                    }
                    //计算距离
                    dict[next] = dict[pos] + 1;
                    //加入待搜索队列
                    queue.Enqueue(next);
                    //添加进队列
                    positions.Add(next);
                }
                return false;
            }

            while (queue.Count > 0)
            {
                Vector2 node = queue.Dequeue();
                //查找4个方向
                if (SearchNext(node, new Vector2(0, 1)) ||
                    SearchNext(node, new Vector2(0, -1)) ||
                    SearchNext(node, new Vector2(-1, 0)) ||
                    SearchNext(node, new Vector2(1, 0)))
                {
                    break;
                }
            }

            return positions;
        }

        private static List<Vector2> GetPath(Vector2 start, Vector2 end, Dictionary<Vector2, int> dict)
        {
            //计算结果路径
            List<Vector2> path = new List<Vector2>();
            path.Add(end); //必须先添加终点, 否则就会陷入起点->第一步, 第一步->起点的死循环中

            //搜索算法
            void AddToPath(Vector2 pos, Vector2 dir, List<PathNode> nodes)
            {
                Vector2 next = pos + dir;
                if (dict.ContainsKey(next))
                {
                    nodes.Add(new PathNode(next, dict[next]));
                }
            }

            while (true)
            {
                Vector2 node = path[0];
                if (node == start)
                {
                    break;
                }
                //将周围最小的点加入路径队列
                List<PathNode> nodes = new List<PathNode>();
                AddToPath(node, new Vector2(0, 1), nodes);
                AddToPath(node, new Vector2(0, -1), nodes);
                AddToPath(node, new Vector2(-1, 0), nodes);
                AddToPath(node, new Vector2(1, 0), nodes);
                //排序(寻找最优解)
                nodes.Sort();
                //没有路可以走
                if (nodes.Count == 0)
                {
                    break;
                }
                path.Insert(0, nodes[0].Position);
            }

            return path;
        }

        private struct PathNode : IComparable
        {
            public Vector2 Position;

            public int Value;

            public PathNode(Vector2 position, int value)
            {
                Position = position;
                Value = value;
            }

            public int CompareTo(object obj)
            {
                PathNode pathNode = (PathNode)obj;
                return Value.CompareTo(pathNode.Value);
            }
        }
    }
}