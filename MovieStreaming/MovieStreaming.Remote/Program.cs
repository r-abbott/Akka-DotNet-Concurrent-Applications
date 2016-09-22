using Akka.Actor;
using MovieStreaming.Common;
using System.Threading.Tasks;

namespace MovieStreaming.Remote
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem in remote process");

            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            Task.WaitAll(MovieStreamingActorSystem.WhenTerminated);
        }
    }
}
