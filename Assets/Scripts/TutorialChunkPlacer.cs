using System;

public class TutorialChunkPlacer : ChunkPlacer
{
    private TutorialUIViewer _tutorialUIViewer;
    
    protected override void Awake()
    {
        _tutorialUIViewer = GetTutorialUIViewer();
        
        base.Awake();
    }


    protected override void SpawnNextChunkInSequence()
    {
        switch (SpawnedChunks.Count)
        {
            case 1:
                SpawnEmptyChunk();
                _tutorialUIViewer.ShowTutorialControlWindow();
                
                break;
            case 2:
                SpawnChunkWithCollectables();

                break;
            case 3:
                SpawnEmptyChunk();
                _tutorialUIViewer.ShowTutorialCollectingWindow();

                break;
            case 4:
                SpawnChunkWithEnemies();

                break;
            case 5:
                SpawnEmptyChunk();
                _tutorialUIViewer.ShowTutorialAliensWindow();

                break;
            case 6:
                SpawnLandscapeChunk();

                break;
            case 7:
                SpawnEmptyChunk();
                _tutorialUIViewer.ShowTutorialObstaclesWindow();

                break;
            case 8:
                SpawnTornadoChunk();

                break;
            case 9:
                SpawnFinishChunk();
                _tutorialUIViewer.ShowTutorialTornadoWindow();

                break;
            default:
                throw new InvalidOperationException();
        }
    }

    private TutorialUIViewer GetTutorialUIViewer()
    {
       UIRoot uiRoot = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot().GetComponent<UIRoot>();

       return uiRoot.GetTutorialUIViewer();
    }
}
