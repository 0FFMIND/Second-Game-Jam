using UnityEngine;

public class TransitionSetup : MonoBehaviour
{
    const int MaxSortingOrder = 32767;
    const float DistanceFromNearClipPlane = 0.00001f;

    private Camera _transitionCamera;

    public GameObject SetUpTransition(TransitionType transitionType, int widthOfTransitionInBlocks, float transitionBlockAnimationTime, Sprite transitionSprite, Color transitionBlockColor, GameObject transitionBlockPrefab, GameObject transitionOrdererPrefab, int skipEveryXBlockUpdates, Camera transitionCamera)
    {
        _transitionCamera = transitionCamera;

        //Want an odd numbers for transition blocks
        widthOfTransitionInBlocks = MakeOdd(widthOfTransitionInBlocks);

        float transitionBlockScale = GetScaleOfTransitionBlock(widthOfTransitionInBlocks);
        int heightOfTransitionInBlocks = GetNumberOfTransitionBlocksHeight(transitionBlockScale);

        GameObject transitionBlockObject = SetUpTransitionBlock(transitionType, transitionBlockAnimationTime, transitionSprite, transitionBlockColor, transitionBlockPrefab, skipEveryXBlockUpdates);
        GameObject transitionOrdererObject = SetUpTransitionOrderer(transitionOrdererPrefab);

        if (transitionBlockObject == null || transitionOrdererObject == null)
        {
            return null;
        }

        TransitionOrderBase transitionOrderer = transitionOrdererObject.GetComponent<TransitionOrderBase>();

        transitionOrderer.Setup(widthOfTransitionInBlocks, heightOfTransitionInBlocks, transitionBlockScale, transitionType, transitionBlockObject, _transitionCamera);
        Destroy(transitionBlockObject);
        AttachTransitionToMainCamera(transitionOrdererObject.transform, widthOfTransitionInBlocks * transitionBlockScale);

        return transitionOrdererObject;
    }

    private bool CheckIfAnimationIsUsed(AnimationCurve animationCurve)
    {
        if(animationCurve.length < 2)
        {
            return false;
        }

        if (animationCurve.length == 2 && (animationCurve.keys[0].value == animationCurve.keys[1].value))
        {
            return false;
        }

        return true;
    }

    private GameObject SetUpTransitionBlock(TransitionType transitionType, float transitionBlockAnimationTime, Sprite transitionSprite, Color transitionBlockColor, GameObject transitionBlockPrefab, int skipEveryXBlockUpdates)
    {
        GameObject transitionBlockObject = (GameObject)Instantiate(transitionBlockPrefab, transform.position, Quaternion.identity);
        AttachTransformToThisObject(transitionBlockObject.transform);

        TransitionBlock transitionBlock = transitionBlockObject.GetComponent<TransitionBlock>();
        if (transitionBlock == null)
        {
            Debug.LogWarning("Transition block prefab doesn't have a TransitionBlock script please make sure it has one");
            return null;
        }
        transitionBlock._animationTime = transitionBlockAnimationTime;
        transitionBlock._transitionType = transitionType;
        transitionBlock._skipEveryXBlockUpdates = skipEveryXBlockUpdates;

        transitionBlock._useScale = CheckIfAnimationIsUsed(transitionBlock._xScaleOverTime) || CheckIfAnimationIsUsed(transitionBlock._yScaleOverTime);
        transitionBlock._usePosition = CheckIfAnimationIsUsed(transitionBlock._xPositionOverTime) || CheckIfAnimationIsUsed(transitionBlock._yPositionOverTime);
        transitionBlock._useRotation = CheckIfAnimationIsUsed(transitionBlock._rotationOverTime);
        transitionBlock._useAlpha = CheckIfAnimationIsUsed(transitionBlock._alphaOverTime);

        SpriteRenderer transitionBlockRenderer = transitionBlockObject.GetComponent<SpriteRenderer>();
        if (transitionBlockRenderer == null)
        {
            Debug.LogWarning("Transition block prefab doesn't have a SpriteRenderer script please make sure it has one");
            return null;
        }
        SortingLayer[] sortingLayers = SortingLayer.layers;
        transitionBlockRenderer.sortingLayerName = sortingLayers[sortingLayers.Length - 1].name;
        transitionBlockRenderer.sortingOrder = MaxSortingOrder;
        transitionBlockRenderer.color = transitionBlockColor;
        if (transitionSprite != null)
        {
            transitionBlockRenderer.sprite = transitionSprite;
        }

        return transitionBlockObject;
    }

    private GameObject SetUpTransitionOrderer(GameObject transitionOrdererPrefab)
    {
        GameObject transitionOrdererObject = (GameObject)Instantiate(transitionOrdererPrefab, Vector3.zero, Quaternion.identity);
        TransitionOrderBase transitionOrderer = transitionOrdererObject.GetComponent<TransitionOrderBase>();
        if (transitionOrderer == null)
        {
            Debug.LogWarning("The transition orderer object does not have a transition orderer script attached");
            return null;
        }
        return transitionOrdererObject;
    }

    private int GetNumberOfTransitionBlocksHeight(float transitionBlockScale)
    {
        float cameraHeightUnityUnits = _transitionCamera.orthographicSize * 2.0f;
        cameraHeightUnityUnits = AddPaddingToCameraValue(cameraHeightUnityUnits);
        int numberOfTransitionBlocksHeight = Mathf.CeilToInt(cameraHeightUnityUnits / transitionBlockScale);

        //Want an odd numbers for transition blocks
        numberOfTransitionBlocksHeight = MakeOdd(numberOfTransitionBlocksHeight);

        return numberOfTransitionBlocksHeight;
    }

    private float GetScaleOfTransitionBlock(int widthOfTransitionInBlocks)
    {
        float cameraHeightUnityUnits = _transitionCamera.orthographicSize * 2.0f;
        float cameraWidthUnityUnits = cameraHeightUnityUnits * _transitionCamera.aspect;
        cameraWidthUnityUnits = AddPaddingToCameraValue(cameraWidthUnityUnits);
        return cameraWidthUnityUnits / widthOfTransitionInBlocks;
    }

    private int MakeOdd(int number)
    {
        if (number % 2 == 0)
        {
            return number + 1;
        }
        return number;
    }

    private void AttachTransitionToMainCamera(Transform transitionTransform, float widthOfTransitionInUnits)
    {
        Transform mainCameraTransform = _transitionCamera.transform;
   
        transitionTransform.parent = mainCameraTransform;
        
        Vector3 modifiedPosition = mainCameraTransform.position;
        modifiedPosition += mainCameraTransform.forward * (_transitionCamera.nearClipPlane + DistanceFromNearClipPlane);
        transitionTransform.position = modifiedPosition;

        transitionTransform.rotation = mainCameraTransform.rotation;

        transitionTransform.localScale = GetTransitionScaleBasedOnCamera(widthOfTransitionInUnits);
    }

    private Vector3 GetTransitionScaleBasedOnCamera(float widthOfTransitionInUnits)
    {
        if(_transitionCamera.orthographic)
        {
            return new Vector3(1.0f, 1.0f, 1.0f);
        }

        //Math from https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        float heightOfScreenInUnits = 2.0f * (_transitionCamera.nearClipPlane + DistanceFromNearClipPlane) * Mathf.Tan(_transitionCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float widthOfScreenInUnits = heightOfScreenInUnits * _transitionCamera.aspect;
        widthOfScreenInUnits = AddPaddingToCameraValue(widthOfScreenInUnits);
        float newScale = widthOfScreenInUnits / widthOfTransitionInUnits;
        return new Vector3(newScale, newScale, 1.0f);
    }

    private float AddPaddingToCameraValue(float cameraValue)
    {
        return cameraValue * 1.1f;
    }

    private void AttachTransformToThisObject(Transform childTransform)
    {
        childTransform.parent = transform.parent;
    }
}
