using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmberMathBase.Trigonometry {

    public class UnitCircle {
        public static readonly float FullCircle = 2.0f * Mathf.PI;

        public enum RotationDirection {
            Clockwise = -1,
            Counterclockwise = 1,
        }

        public static Vector2 CircumferencePointForAngle(float angle) {
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        public static float VectorToAngle(Vector2 vec) {
            return NormalizeAngle(Mathf.Atan2(vec.y, vec.x));
        }

        public static float AngleBetweenTwoAgles(float start, float end, RotationDirection directionOfRotation) {
            bool needsToCrossZero = directionOfRotation == RotationDirection.Counterclockwise
                ? end < start
                : end > start;

            return needsToCrossZero
                ? (
                    directionOfRotation == RotationDirection.Counterclockwise
                        ? end + FullCircle - start
                        : -1.0f * (start + FullCircle - end)
                    )
                : Mathf.Abs(end - start);
        }

        public static bool IsBetweenAngles(float start, float end, float angle, RotationDirection directionOfRotation) {
            return AngleBetweenTwoAgles(start, angle, directionOfRotation) < AngleBetweenTwoAgles(start, end, directionOfRotation);
        }

        public static float MinAngleBetweenTwoAngles(float a, float b) {
            return Mathf.Min(
                Mathf.Abs(AngleBetweenTwoAgles(a, b, RotationDirection.Counterclockwise)),
                Mathf.Abs(AngleBetweenTwoAgles(a, b, RotationDirection.Clockwise))
            );
        }

        public static float NormalizeAngle(float angle) {
            float singleRound = angle % FullCircle;
            return singleRound > 0.0f
                ? singleRound
                : FullCircle - Mathf.Abs(singleRound);
        }
    }
}
