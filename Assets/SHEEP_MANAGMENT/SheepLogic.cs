using UnityEngine;

namespace Ursaanimation.CubicFarmAnimals
{
    public class SheepLogic : MonoBehaviour
    {
        public Animator animator;
        public string walkForwardAnimation = "walk_forward";
        public string walkBackwardAnimation = "walk_backwards";
        public string runForwardAnimation = "run_forward";
        public string turn90LAnimation = "turn_90_L";
        public string turn90RAnimation = "turn_90_R";
        public string trotAnimation = "trot_forward";
        public string sittostandAnimation = "sit_to_stand";
        public string standtositAnimation = "stand_to_sit";

        private float _timer;
        public float MoveSpeed;
        private Vector3 direction;
        public int BaseHealth;
        public int CurrentHealth;
        public RectTransform healthBar;

        public int CurrentAge;
        private float SpeedMultiplier;

        void Start()
        {
            animator = GetComponent<Animator>();
            direction = Vector3.forward;
            _timer = 3;
            CurrentHealth = BaseHealth;
        }

        void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > 3)
            {
                NewAction();
                _timer = 0;
            }

            GetComponent<Rigidbody>().linearVelocity = direction * MoveSpeed * SpeedMultiplier;
            GrowSheepUp();

            if (CurrentHealth <=0)
            {
                GameManager.Instance.SheepDied(this);
                Destroy(this);
            } else
            {
                float healthPercent = (float)CurrentHealth / BaseHealth;
                healthBar.localScale = new Vector3(healthPercent, 1, 1);
            }
        }

        private void NewAction()
        {
            int randomAction = Random.Range(0, 5);

            if (randomAction == 0)
            {
                animator.Play(walkForwardAnimation);
                direction = transform.forward;
                SpeedMultiplier = 1;
                return;
            }
            if (randomAction == 1)
            {
                animator.Play(runForwardAnimation);
                direction = transform.forward;
                SpeedMultiplier = (float)(1.5);
                return;
            }
            if (randomAction == 2)
            {
                animator.Play(trotAnimation);
                direction = transform.forward;
                SpeedMultiplier = (float)(0.75);
                return;
            }
            if (randomAction == 3)
            {
                animator.Play(turn90RAnimation);
                RotateSmooth(90);
                SpeedMultiplier = 0;
                return;
            }
            if (randomAction == 4)
            {
                animator.Play(turn90LAnimation);
                RotateSmooth(-90);
                SpeedMultiplier = 0;
                return;
            }

            return;
        }

        private void RotateSmooth(float angle)
        {
            Quaternion newRotation = Quaternion.Euler(0, angle, 0) * transform.rotation;
            transform.rotation = newRotation;
            direction = transform.forward;
        }

        public void GrowSheepUp()
        {
            transform.localScale = new Vector3((float)(0.5 + (CurrentAge * 0.2)), (float)(0.5 + (CurrentAge * 0.2)), (float)(0.5 + (CurrentAge * 0.2)));
        }
    }
}
