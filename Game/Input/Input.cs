using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Input;

namespace Sim.Input
{
    public interface IKeyboardHandler
    {
        void OnKeyDown(KeyboardKeyEventArgs eventArgs);

        void OnKeyUp(KeyboardKeyEventArgs eventArgs);
    }

    public class InputSystem : IKeyboardHandler
    {
        private KeyMappings mappings = new();
        private readonly List<InputContext> contexts = new();

        public void AddContext(InputContext context)
        {
            contexts.Add(context);
        }

        public void RemoveContext(InputContext context)
        {
            contexts.Remove(context);
        }

        public void OnKeyDown(KeyboardKeyEventArgs eventArgs)
        {
            Console.WriteLine($"OnKeyDown {eventArgs.Key} {eventArgs.IsRepeat}");

            var availableTriggers = mappings.GetMappingsByKey(eventArgs.Key).Select(mapping => mapping.Trigger);

            for (var i = 0; i < contexts.Count; i++)
            {
                var currentContext = contexts[i];
                foreach (var availableTrigger in availableTriggers)
                {
                    if (currentContext.actions.ContainsKey(availableTrigger))
                    {
                        currentContext.actions[availableTrigger].OnTrigger();
                        return;
                    }

                    if (currentContext.states.ContainsKey(availableTrigger))
                    {
                        if (eventArgs.IsRepeat)
                        {
                            currentContext.states[availableTrigger].OnRepeat();
                        }
                        else
                        {
                            currentContext.states[availableTrigger].OnStart();
                        }
                        return;
                    }
                }
            }
        }

        public void OnKeyUp(KeyboardKeyEventArgs eventArgs)
        {
            Console.WriteLine($"onKeyUp {eventArgs.Key}");

            var availableTriggers = mappings.GetMappingsByKey(eventArgs.Key).Select(mapping => mapping.Trigger);

            for (var i = 0; i < contexts.Count; i++)
            {
                var currentContext = contexts[i];
                foreach (var availableTrigger in availableTriggers)
                {
                    if (currentContext.states.ContainsKey(availableTrigger))
                    {
                        currentContext.states[availableTrigger].OnStop();
                        return;
                    }
                }
            }
        }
    }

    public class InputContext
    {
        public Dictionary<string, InputAction> actions = new();
        public Dictionary<string, InputState> states = new();
        public Dictionary<string, InputRange> ranges = new();

        public void RegisterAction(string trigger, InputAction action)
        {
            actions.Add(trigger, action);
        }

        public void RegisterState(string trigger, InputState state)
        {
            states.Add(trigger, state);
        }

        public void RegisterRange(string trigger, InputRange range)
        {
            ranges.Add(trigger, range);
        }
    }

    // An action is a single-time thing, like casting a spell or opening a door; generally if the
    // player just holds the button down, the action should only happen once, generally when the
    // button is first pressed, or when it is finally released. "Key repeat" should not affect
    // actions.
    public interface InputAction
    {
        void OnTrigger();
    }

    // States are similar to actions, but designed for continuous activities, like running or
    // shooting. A state is a simple binary flag: either the state is on, or it's off. When the
    // state is active, the corresponding game action is performed; when it is not active, the
    // action is not performed. Simple as that. Other good examples of states include things like
    // scrolling through menus.
    public interface InputState
    {
        void OnStart();
        void OnRepeat();
        void OnStop();
    }

    // Finally, a range is an input that can have a number value associated with it. For
    // simplicity, we will assume that ranges can have any value; however, it is common to define
    // them in normalized spans, e.g. 0 to 1, or -1 to 1. Ranges are most useful for dealing with
    // analog input, such as joysticks, analog controller thumbsticks, and mice.
    public interface InputRange
    {
        void OnValueChange();
        void OnValueStart();
        void OnValueEnd();
    }
}