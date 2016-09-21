using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");

            Receive<PlayMovieMessage>(m => HandlePlayMovieMessage(m));
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteLineYellow(
                $"PlayMovieMessage {message.MovieTitle} for user {message.UserId}");
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen($"PlaybackActor PreRestart because: {reason}");

            base.PreRestart(reason, message);
        }
    }
}
