using UnityEngine;

namespace GameFolders.Scripts
{
    public class DirectionProvider : BaseBehaviour
    {
        [Header("Provided Directions")]
        public Direction firstDirection;
        public Direction secondDirection;
        
        [Header("End Positions")]
        public Vector3 targetPosForStartToEnd;
        public Vector3 targetPosForEndToStart;
    }
}
