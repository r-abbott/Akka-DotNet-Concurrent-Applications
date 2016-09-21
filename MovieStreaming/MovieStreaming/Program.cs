using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Thread.Sleep(1000);
            Console.WriteLine("Actor system created");

            Props playbackActorProps = Props.Create<UserActor>();
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Sending PlayMovieMessage (Boolean Lies)");
            playbackActorRef.Tell(new PlayMovieMessage("Boolean Lies", 77));
            Console.ReadKey();
            Console.WriteLine("Sending another PlayMovieMessage (Codenan the Destroyer)");
            playbackActorRef.Tell(new PlayMovieMessage("Codenan the Destroyer", 1));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            playbackActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending another StopMovieMessage");
            playbackActorRef.Tell(new StopMovieMessage());

            Terminate();
        }

        static void Terminate()
        {
            Console.ReadKey();
            Task.WaitAll(
                MovieStreamingActorSystem.Terminate(),
                MovieStreamingActorSystem.WhenTerminated
                );
            Console.WriteLine("Actor system shutdown");
            Console.ReadKey();
        }
    }
}
