using Akka.Actor;
using MovieStreaming.Messages;
using System;
using System.Threading.Tasks;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            Receive<StopMovieMessage>(message => HandleStopMovieMessage());
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            if(_currentlyWatching != null)
            {
                ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping existing one");
            }
            else
            {
                StartPlayingMovie(message.MovieTitle);
            }
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;

            ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'");
        }

        private void HandleStopMovieMessage()
        {
            if (_currentlyWatching == null)
            {
                ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing");
            }
            else
            {
                SoptPlayingCurrentMovie();
            }
        }

        private void SoptPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching '{_currentlyWatching}'");
            _currentlyWatching = null;
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen($"UserActor PreRestart because: {reason}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen($"UserActor PostRestart because: {reason}");

            base.PostRestart(reason);
        }
    }
}
