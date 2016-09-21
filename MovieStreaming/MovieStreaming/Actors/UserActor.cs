using Akka.Actor;
using MovieStreaming.Messages;
using System;
using System.Threading.Tasks;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private int _id;
        private string _currentlyWatching;

        public UserActor(int userId)
        {
            _id = userId;
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message => ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping existing one"));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
            ColorConsole.WriteLineYellow("UserActor has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing"));
            ColorConsole.WriteLineYellow("UserActor has now become Stopped");
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'");

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(movieTitle));

            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching '{_currentlyWatching}'");
            _currentlyWatching = null;
            Become(Stopped);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow($"UserActor PreRestart because: {reason}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow($"UserActor PostRestart because: {reason}");

            base.PostRestart(reason);
        }
    }
}
