using UnityEngine;
public static class Library
{
    // Method that does proper rotation around Y axis
    public static void RotateTowards(Transform target, Transform transform, float rotationSpeed)
    {
        // Getting the direction
        Vector3 direction = (target.position - transform.position).normalized;

        // Flattinng Vector3 axis
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
