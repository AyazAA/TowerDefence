using System;
using UnityEngine;

    // 
    /*
     * To find the firing direction, it is necessary to solve this system of equations
     * Xm + t * Xdm = Xp + t * Xdp
     * Ym + t * Ydm = Yp + t * Ydp
     * Zm + t * Zdm = Zp + t * Zdp
     * (Xdp^2 + Ydp^2 + Zdp^2)^0.5 = Vp
     *
     * where, (Xm, Ym, Zm) - start monster position; (Xdm, Ydm, Zdm) - enemy velocity; 
     *  (Xp, Yp, Zp) - start projectile position; (Xdp, Ydp, Zdp) - firing direction;
     *  t - time of meeting projectile and monster; Vp - projectile speed
     *
     * First we find t, then substituting it into the first three equations we find the direction of firing
     *
     * The formula for finding t   https://drive.google.com/file/d/1UmnsNeCh7D5Z3l8LMkyXqHjzxUGNE_Df/view?usp=sharing
     * where (Xm, Ym, Zm) = (a,b,c); (Xdm, Ydm, Zdm) = (d, e, f); (Xp, Yp, Zp) = (h, g, i); Vp = j; t = x
     */

public static class ShootDirectionCalculator
{
    public static Vector3 Calculate(Vector3 enemyPosition, Vector3 projPosition, Vector3 enemyVelocity, float projSpeed, float maxLengthDirection)
    {
        Vector3 calculatedDirection = Vector3.zero;
        
        float divisor = (MathF.Pow(enemyVelocity.x, 2) + MathF.Pow(enemyVelocity.y, 2) +
            MathF.Pow(enemyVelocity.z, 2) - MathF.Pow(projSpeed, 2));
        
        float s1 = (2 * enemyPosition.x * enemyVelocity.x + 2 * enemyPosition.y * enemyVelocity.y +
                    2 * enemyPosition.z * enemyVelocity.z - 2 * enemyVelocity.x * projPosition.x -
                    2 * enemyVelocity.y * projPosition.y - 2 * enemyVelocity.z * projPosition.z)
                   / (2 * divisor);
        
        float inSqrt = MathF.Pow(s1, 2) -
                       (MathF.Pow(enemyPosition.x, 2) + MathF.Pow(enemyPosition.y, 2) +
                        MathF.Pow(enemyPosition.z, 2) - 2 * enemyPosition.x * projPosition.x +
                        MathF.Pow(projPosition.x, 2) - 2 * enemyPosition.y * projPosition.y +
                        MathF.Pow(projPosition.y, 2) - 2 * enemyPosition.z * projPosition.z +
                        MathF.Pow(projPosition.z, 2)) / divisor;
        if (inSqrt < 0)
        {
            Debug.Log("Sqrt less than 0");
            calculatedDirection = Vector3.zero;
            return calculatedDirection;
        }
        
        float sqrt1 = -MathF.Sqrt(inSqrt);
        float sqrt2 = MathF.Sqrt(inSqrt);
        
        float meetTime1 = -s1 - sqrt1;
        float meetTime2 = -s1 - sqrt2;
        float meetTime = Mathf.Min(meetTime1, meetTime2);
        if (meetTime <= 0)
        {
            meetTime = Mathf.Max(meetTime1, meetTime2);
            if (meetTime <= 0)
            {
                Debug.Log("Time less than 0");
                calculatedDirection = Vector3.zero;
                return calculatedDirection;
            }
        }

        float x = (enemyPosition.x + meetTime * enemyVelocity.x - projPosition.x) / meetTime;
        float y = (enemyPosition.y + meetTime * enemyVelocity.y - projPosition.y) / meetTime;
        float z = (enemyPosition.z + meetTime * enemyVelocity.z - projPosition.z) / meetTime;
        Vector3 dir = new Vector3(x, y, z);
        float len = dir.magnitude * meetTime;

        if (len > maxLengthDirection)
        {
            Debug.Log("Len dir more than range");
            calculatedDirection = Vector3.zero;
            return calculatedDirection;
        }
        
        calculatedDirection = dir;
        Debug.Log($" predictiveDirection {calculatedDirection}");
        return calculatedDirection;
    }
}