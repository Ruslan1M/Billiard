using Logic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private BallManager m_BallManager;

    public BallManager BallManager => m_BallManager;
    
    public void Init(BallManager ballManager)
    {
        m_BallManager = ballManager;
    }
}
