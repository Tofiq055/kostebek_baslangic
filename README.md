# AR Hand-Tracking Whack-a-Mole (Köstebek Oyunu)

A unique Augmented Reality (AR) arcade game where you interact with the game world using your real hands! Built with **Unity** and **MediaPipe**, this project demonstrates high-performance hand tracking on Android devices without external hardware controls.

## 🎮 Features
*   **Real-time Hand Tracking:** Uses MediaPipe to detect your hand and index finger position with low latency.
*   **Touchless Interaction:** Physical interaction in AR space (Raycasting from finger tip).
*   **Satisfying Game Loop:** 
    *   Random mole spawning system.
    *   Dynamic "Spin & Shrink" hit death animations.
    *   Instant visual feedback (Red Flash).
*   **UI Polish:** Animated Scoring system that pulses on every point.
*   **Cross-Platform Ready:** Optimized for Android (ARM64) with automatic camera mirroring correction.

## 📱 Tech Stack & Requirements
*   **Engine:** Unity 2022.3 LTS
*   **Core Plugin:** [MediaPipe Unity Plugin](https://github.com/homuler/MediaPipeUnityPlugin) (v0.16.3)
*   **Target Platform:** Android (Requires ARM64 Device, e.g., Samsung Galaxy A72+)
*   **Scripting:** C# (IL2CPP Backend)

## 🛠️ Installation & Setup

1.  **Clone the Repository**
    ```bash
    git clone https://github.com/Tofiq055/kostebek_baslangic.git
    ```

2.  **Open in Unity**
    *   Launch Unity Hub and add the project folder.
    *   Open with Unity 2022.3.62f1 (or compatible LTS version).

3.  **Critical MediaPipe Setup (Important!)**
    *   Before building, you must ensure the MediaPipe models are in `StreamingAssets`.
    *   If available, check `Tools > MediaPipe > Copy Assets`.
    *   *Manual Fix:* If you see "Failed to load hand_landmarker.bytes" on android, manually copy `hand_landmarker.bytes` from the Packages folder to `Assets/StreamingAssets`.

4.  **Build Settings**
    *   Go to **File > Build Settings**.
    *   Switch to **Android**.
    *   Ensure **Player Settings > Basic > Scripting Backend** is set to **IL2CPP**.
    *   Ensure **Target Architecture** is **ARM64**.
    *   Ensure **Active Input Handling** is set to **Both** (New & Old System).

## 🕹️ How to Play
1.  Launch the app on your Android Phone.
2.  Grant Camera permissions.
3.  Show your hand to the camera. You will see a **Red Tracking Sphere** on your index finger.
4.  Move your finger over a rising mole to whack it!
5.  Gain points and watch the score pop!

## 🤝 Contributing
Feel free to submit issues or pull requests. For major changes, please open an issue first to discuss what you would like to change.

## 📄 License
This project is open source.
