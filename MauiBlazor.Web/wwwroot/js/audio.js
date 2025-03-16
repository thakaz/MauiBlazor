// 音声再生用のJavaScript関数

// システム音（ビープ音）を再生する関数
function playBeep() {
    try {
        // Web Audio APIを使用してビープ音を生成
        const audioContext = new (window.AudioContext || window.webkitAudioContext)();
        const oscillator = audioContext.createOscillator();
        const gainNode = audioContext.createGain();
        
        oscillator.type = 'sine';
        oscillator.frequency.value = 800; // 800Hzのビープ音
        gainNode.gain.value = 0.1; // 音量を調整
        
        oscillator.connect(gainNode);
        gainNode.connect(audioContext.destination);
        
        oscillator.start();
        
        // 0.3秒後に停止
        setTimeout(() => {
            oscillator.stop();
            audioContext.close();
        }, 300);
    } catch (error) {
        console.error('ビープ音再生エラー:', error);
    }
}

// 音声データを再生する関数
function playAudio(audioDataUrl) {
    return new Promise((resolve, reject) => {
        try {
            const audio = new Audio(audioDataUrl);
            
            audio.onended = () => {
                resolve();
            };
            
            audio.onerror = (error) => {
                console.error('音声再生エラー:', error);
                reject(error);
            };
            
            // 音声を再生
            audio.play().catch(error => {
                console.error('音声再生エラー:', error);
                reject(error);
            });
        } catch (error) {
            console.error('音声再生エラー:', error);
            reject(error);
        }
    });
}

// グローバルスコープに関数を公開
window.playBeep = playBeep;
window.playAudio = playAudio;
