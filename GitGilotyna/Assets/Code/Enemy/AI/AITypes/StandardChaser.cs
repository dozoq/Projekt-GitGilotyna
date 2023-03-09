namespace Code.Enemy.AITypes
{
    public class StandardChaser : AIType
    {
        public override string Name => nameof(StandardChaser);
        public sealed override void Reapeat()
        {
            UpdatePath();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdatePath()
        {
            if (ctx.seeker.IsDone())
                ctx.seeker.StartPath(ctx.rigidbody2D.position, ctx.target.position, base.OnPathCompleted);
        }

        
    }
}