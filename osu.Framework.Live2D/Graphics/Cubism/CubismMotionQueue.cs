using System.Collections.Generic;
using CubismFramework;

namespace osu.Framework.Graphics.Cubism
{
    public class CubismMotionQueue
    {
        public CubismMotionQueueEntry Current { get; private set; }
        public bool IsActive => Current?.Playing ?? false;
        private readonly CubismAsset asset;
        private readonly CubismAsset.MotionType type;
        private List<(ICubismMotion, bool)> queue = new List<(ICubismMotion, bool)>();

        public CubismMotionQueue(CubismAsset asset, CubismAsset.MotionType type)
        {
            this.asset = asset;
            this.type = type;
        }

        public void Add(ICubismMotion motion, bool loop = false) => queue.Add((motion, loop));

        public void Next(double fadeOutTime = 0)
        {
            if (queue.Count - 1 > 0)
                Current?.Terminate(fadeOutTime);
        }

        public void Suspend(bool enable = true) => Current?.Suspend(enable);

        public void Stop(double fadeOutTime = 0)
        {
            queue.Clear();
            Next(fadeOutTime);
        }

        public void Update()
        {
            if (((Current?.Finished ?? true) || (Current?.Terminated ?? true)) && (queue.Count > 0))
            {
                var (motion, loop) = queue[0];
                Current = asset.StartMotion(type, motion, loop);

                queue.RemoveAll(entry => entry.Item1 == motion);
            }
        }
    }
}