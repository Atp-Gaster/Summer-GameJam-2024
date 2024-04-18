using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QueueManager : MonoBehaviour
{
    public CustomerSO[] customerlists;
    public Queue<CustomerSO> customerQueue = new Queue<CustomerSO>();
    public List<CustomerSO> seatedCustomers = new List<CustomerSO>();
    public int maxSeats = 1;
    public CustomerSeat[] seats;

    [SerializeField] private Image uifill;
    [SerializeField] private TextMeshProUGUI uiText;

    public int TotalScore;
    [SerializeField] private TextMeshProUGUI uiScore;
    private Dictionary<CustomerSeat, Coroutine> seatCoroutineMap = new Dictionary<CustomerSeat, Coroutine>();

    public void EnqueueCustomer(CustomerSO customer)
    {
        customerQueue.Enqueue(customer);
    }

    public void DequeueCustomer()
    {
        if (customerQueue.Count > 0)
        {
            CustomerSO customer = customerQueue.Dequeue();
        }
        else
        {
            Debug.Log("No customers in queue.");
        }
    }

    public CustomerSO GetRandomCustomer()
    {
        if (customerlists.Length == 0)
        {
            Debug.Log("No customers available.");
            return null;
        }

        int randomIndex = Random.Range(0, customerlists.Length);
        CustomerSO randomCustomer = customerlists[randomIndex];

        customerlists = customerlists.Where((source, index) => index != randomIndex).ToArray();

        return randomCustomer;
    }

    public void AddRandomCustomersToQueue(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CustomerSO randomCustomer = GetRandomCustomer();
            if (randomCustomer != null)
            {
                EnqueueCustomer(randomCustomer);
                RecipeSO randomItem = randomCustomer.GetRandomWishlist();
            }
        }
    }

    private void Start()
    {
        // Assign Queue
        AddRandomCustomersToQueue(3);
        TotalScore = 0;

        for (int i = 0; i < maxSeats; i++)
        {
            seats[i].SetSeatStatus(false);
        }
    }

    private void Update()
    {
        while (customerQueue.Count > 0 && seatedCustomers.Count < maxSeats)
        {
            CustomerSO customer = customerQueue.Dequeue();
            AssignCustomerToSeat(customer);
        }
    }

    private void AssignCustomerToSeat(CustomerSO customer)
    {
        // Debug.Log(seatedCustomers.Count + " customer(s) sitting");
        foreach (CustomerSeat seat in seats)
        {
            if (!seat.IsOccupied())
            {
                Debug.Log(customerQueue.Count + " customer(s) left in the queue");
                seat.SetCustomer(customer);
                seatedCustomers.Add(customer);
                Coroutine coroutine = StartCoroutine(StartTimer(customer.timerDuration, seat));
                seatCoroutineMap[seat] = coroutine;
                return;
            }
        }
    }

    public void RemoveCustomerFromSeat(CustomerSeat seat)
    {
        if (seat.IsOccupied())
        {
            int score = seat.GetScore();
            Debug.Log("Customer " + seat.customer.customerName + " state " + seat.customer.currentState + ". Score: " + score);
            CalculateScore(score);

            seatedCustomers.Remove(seat.customer);
            Coroutine coroutine;
            if (seatCoroutineMap.TryGetValue(seat, out coroutine))
            {
                StopCoroutine(coroutine); // Stop the coroutine using the reference
            }
            seat.RemoveCustomer();

            if (customerQueue.Count > 0)
            {
                AssignCustomerToSeat(customerQueue.Dequeue());
            }
        }
    }

    private void CalculateScore(int score)
    {
        TotalScore += score;
        uiScore.text = TotalScore.ToString();
    }

    private IEnumerator StartTimer(float duration, CustomerSeat seat)
    {
        yield return new WaitForSeconds(3f);

        Debug.Log("Timer started! " + duration);
        float timer = 0f;

        while (timer <= duration)
        {
            uiText.text = Mathf.CeilToInt(duration - timer).ToString();

            uifill.fillAmount = 1 - (timer / duration);

            int remainingTime = Mathf.CeilToInt(duration - timer);

            if (remainingTime >= 16)
            {
                uifill.color = new Color32(0, 255, 11, 168);
                seat.ChangeCustomerState(seat.customer, seat.customer.state[0]);
            }
            else if (remainingTime <= 15 && remainingTime >= 6)
            {
                uifill.color = Color.yellow;
                seat.ChangeCustomerState(seat.customer, seat.customer.state[1]);
            }
            else if(remainingTime <= 5)
            {
                uifill.color = Color.red;
                seat.ChangeCustomerState(seat.customer, seat.customer.state[2]);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Timer has finished
        uiText.text = "0";
        Debug.Log("Timer finished!");
        seat.ChangeCustomerState(seat.customer, seat.customer.state[3]);
        RemoveCustomerFromSeat(seat);
    }
}
