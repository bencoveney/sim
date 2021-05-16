using System;

namespace Sim.Ecs
{
  public class EcsException : Exception
  {
    public EcsException()
    {
    }

    public EcsException(string message)
        : base(message)
    {
    }

    public EcsException(string message, Exception inner)
        : base(message, inner)
    {
    }
  }
}