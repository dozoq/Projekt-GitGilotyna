namespace Code.Enemy.AITypes
{
    public class StandardChaser : AIType
    {
        public override string Name => nameof(StandardChaser);
        public sealed override void Repeat()
        {
            UpdatePath();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdatePath()
        {
            if (_ctx.seeker.IsDone())
                _ctx.seeker.StartPath(_ctx.rigidbody2D.position, _ctx.target.position, base.OnPathCompleted);
        }

        
    }
}