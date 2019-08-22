using MasterData;
using MessagePack.Resolvers;
using UnityEngine;

public static class Initializer {
    [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void SetupMessagePackResolver () {
        CompositeResolver.RegisterAndSetAsDefault (new [] {
            MasterMemoryResolver.Instance, // set MasterMemory generated resolver
                GeneratedResolver.Instance, // set MessagePack generated resolver
                StandardResolver.Instance // set default MessagePack resolver
        });
    }
}
