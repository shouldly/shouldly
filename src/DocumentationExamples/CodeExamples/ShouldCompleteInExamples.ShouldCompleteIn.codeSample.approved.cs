Should.CompleteIn(
                    action: () => { Thread.Sleep(TimeSpan.FromSeconds(2)); },
                    timeout: TimeSpan.FromSeconds(1),
                    customMessage: "Some additional context");