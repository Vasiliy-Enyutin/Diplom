namespace _Project.Scripts.LabyrinthLogic
{
    public class LabyrinthGeneratorCell
    {
        public int X;
        public int Y;

        public bool WallLeft = true;
        public bool WallBottom = true;

        public bool Visited = false;
        public int DistanceFromStart;
    }
}