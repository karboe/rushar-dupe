//-----------------------------------------------------------------------
// <copyright file="PawnManipulator.cs" company="Google">
//
// Copyright 2019 Google LLC All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.ObjectManipulation
{
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;

    /// <summary>
    /// Controls the placement of objects via a tap gesture.
    /// </summary>
    public class PawnManipulator : Manipulator
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR
        /// background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a plane.
        /// </summary>
        public List<GameObject> PawnPrefab;

        /// <summary>
        /// Manipulator prefab to attach placed objects to.
        /// </summary>
        public GameObject ManipulatorPrefab;

        public GameObject PlaneGenerator,PointCloud;

        public int maxObjectsInSceen = 1;
        public int currentObjectsInSeen = 0;

        /// <summary>
        /// Returns true if the manipulation can be started for the given gesture.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        /// <returns>True if the manipulation can be started.</returns>
        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            if (gesture.TargetObject == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Function called when the manipulation is ended.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        protected override void OnEndManipulation(TapGesture gesture)
        {
            if (gesture.WasCancelled)
            {
                return;
            }

            // If gesture is targeting an existing object we are done.
            if (gesture.TargetObject != null)
            {
                return;
            }

            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;

            if (Frame.Raycast(
                gesture.StartPosition.x, gesture.StartPosition.y, raycastFilter, out hit))
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    if (currentObjectsInSeen < maxObjectsInSceen)
                    {
                        // Instantiate game object at the hit pose.

                        //var gameObject = Instantiate(PawnPrefab, hit.Pose.position, hit.Pose.rotation);
                        var manipulator = new GameObject();
                        for (int i = 0; i < PawnPrefab.Count; i++)
                        {
                            if (i == 0)
                            {
                                // var gameObject = Instantiate(PawnPrefab[i], hit.Pose.position, Quaternion.identity);
                                var gameObject = Instantiate(PawnPrefab[i], new Vector3(Camera.main.transform.position.x,hit.Pose.position.y, Camera.main.transform.position.z), Quaternion.identity);
                                // gameObject.transform.position = new Vector3(0, 0, 0);
                                // Instantiate manipulator.
                                manipulator =
                                   Instantiate(ManipulatorPrefab, new Vector3(Camera.main.transform.position.x, hit.Pose.position.y, Camera.main.transform.position.z), Quaternion.identity);

                                // Make game object a child of the manipulator.
                                gameObject.transform.parent = manipulator.transform;
                            }
                            else
                            {
                                Instantiate(PawnPrefab[i], hit.Pose.position, hit.Pose.rotation);

                            }
                        }



                        // Create an anchor to allow ARCore to track the hitpoint as understanding of
                        // the physical world evolves.
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                        // Make manipulator a child of the anchor.
                        manipulator.transform.parent = anchor.transform;

                        // Select the placed object.
                        manipulator.GetComponent<Manipulator>().Select();
                        currentObjectsInSeen++;
                        PlaneGenerator.SetActive(false);
                        PointCloud.SetActive(false);
                    }

                }
            }
        }
    }
}
