using Akka.Actor;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (!_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts.Add(message.MovieTitle, 0);
            }
            _moviePlayCounts[message.MovieTitle]++;

            ColorConsole.WriteMagenta($"MoviePlayerCounterActor '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times");
        }
    }
}