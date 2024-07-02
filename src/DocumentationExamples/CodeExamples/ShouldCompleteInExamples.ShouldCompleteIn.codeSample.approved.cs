Should.CompleteIn(
                    action: () => { Thread.Sleep(TimeSpan.FromSeconds(15)); },
                    timeout: TimeSpan.FromSeconds(0.5),
                    customMessage: "Some additional context");