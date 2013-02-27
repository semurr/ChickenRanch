using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Chicken
{
    class AudioManager
    {
         //sound effect variables
        public SoundEffect backgroundTitle,backgroundPlay; //one theme song for the entire game 
       // bool backgroundPlaying = false;
        public SoundEffectInstance backgroundInstance = null;
        public SoundEffectInstance menuSFXInstance = null; //will hold menu sfx that do not overlap, backgroundchickencluck,crickets_2
        public SoundEffectInstance sFXeggSlipInstance = null; //allow control of the eggslip sfx to avoid overlapping
        private SoundEffectInstance sFXInstance = null;
        public SoundEffect buttonClickSound, roosterAttackSound,roosterSpawnSound, foxAttackSound, eggCrackSound, eggSlipSound,
                            chickenSpawnSound,chickenEggLaySound,chickenNoises,endDaySound, victoryLossSound,itemCollectSound;
        public static AudioManager instance;
        public bool musicOn = true;
        public bool sFXOn = true;
        
        

        public AudioManager(ContentManager content)
        {
            //load audio content
            //***change audio files
            backgroundTitle = content.Load<SoundEffect>("Audio/backgroundMelodySimple[2]"); //main menu music
            //backgroundCredits = content.Load<SoundEffect>("SoundFiles/Theme");//menu music, changes based on win/loss
            backgroundPlay = content.Load<SoundEffect>("Audio/backgroundMelodySimple[3]chirps");//music while in gameplay
            

            buttonClickSound = content.Load<SoundEffect>("Audio/buttonPush");//or can use buttonPushSound
            roosterAttackSound = content.Load<SoundEffect>("Audio/roosterWarning");
            roosterSpawnSound = content.Load<SoundEffect>("Audio/roosterCrow");
            foxAttackSound = content.Load<SoundEffect>("Audio/foxWarning");
            eggCrackSound = content.Load<SoundEffect>("Audio/eggCrack_2");
            eggSlipSound = content.Load<SoundEffect>("Audio/eggSlipSound");
            chickenEggLaySound = content.Load<SoundEffect>("Audio/shortchickencluck");
            chickenSpawnSound = content.Load<SoundEffect>("Audio/softChickenCluck"); //or can use backgroundchickencluck
            chickenNoises = content.Load<SoundEffect>("Audio/backgroundchickencluck");
            endDaySound = content.Load<SoundEffect>("Audio/crickets_2"); //or can use crickets_1
            victoryLossSound = content.Load<SoundEffect>("Audio/Gong_1");
            itemCollectSound = content.Load<SoundEffect>("Audio/XyloNote");
            instance = this;
        }
        //set a different sound based on win/lossd
        public void setBackgroundCreditsSound(SoundEffect sound)
        {
            backgroundPlay = sound;
        }
        public void pauseBackgroundSound()
        {
            backgroundInstance.Pause();
        }
        public void resumeBackgroundSound()
        {
            backgroundInstance.Resume();
        }
        public void stopBackgroundSound()
        {
            backgroundInstance.Stop();
        }
        public void setmenuInstance(SoundEffect sound)
        {
            menuSFXInstance = sound.CreateInstance() ;
        }
        public void setSlipEffectInstance(SoundEffect sound)
        {
            sFXeggSlipInstance = sound.CreateInstance();
        }
        public void stopMenuInstance()
        {
            menuSFXInstance.Stop();
        }
        public void playBackgroundSound(Game1.GameState screen)
        {
            
            switch (screen)
            {
                case Game1.GameState.start:
                    if (backgroundInstance == null)
                    {   // for seamless audio transitions between menu screens
                       
                        backgroundInstance = backgroundTitle.CreateInstance();
                        backgroundInstance.IsLooped = true;
                        if (musicOn == true)
                        {
                            if (backgroundInstance.State != SoundState.Playing)
                            {

                                backgroundInstance.Play();
                            }
                        }
                    }
                  
                    break;
                case Game1.GameState.help:
                    
                  
                    break;
                case Game1.GameState.options:
                    
                  
                    break;
                case Game1.GameState.credits:
                    
                  
                    break;
                case Game1.GameState.game:
                    if (backgroundInstance != null)
                    {
                        backgroundInstance.Stop();//ensures only 1 instance is playing at a time
                    }
                    backgroundInstance = backgroundPlay.CreateInstance();//different background Music
                    backgroundInstance.IsLooped = true;
                    if (musicOn == true && backgroundInstance.State != SoundState.Playing)
                    {
                        
                            backgroundInstance.Play();
                        
                    }
                    break;

                    
            }
            
        }
    }
    
}
