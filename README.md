# *******************************************
# Raffle App Information
# *******************************************

## Description:
This codebase comprises a raffle application developed for an upcoming event. The raffle system allows attendees to purchase tickets for participation in the draw. These tickets are linked to potential rewards based on the numbers matched in the draw.

### Functionality Overview:
- Attendees can acquire tickets for a chance to win.
- Each ticket holds a combination of five randomly generated and unique numbers within the range of 1 to 15.
- Users can purchase up to a maximum of 5 tickets per draw.
- The cost of each ticket is set at $5.
- The raffle starts with an initial seed fund of $100.

### Pot Calculation:
- The total pot size for the draw includes the initial seed funding and the contributions from ticket sales.
- All proceeds from ticket sales directly contribute to the pot. For instance, if 20 tickets are sold, the total pot size will be calculated as $100 (seed funding) + 20 tickets * $5 = $200.

### Rewards Structure:
| Prize Group       | Numbers Matched   | Reward           |
| ----------------- |:-----------------:| ----------------:|
| Group 2           | 2 winning numbers | 10% of total pot |
| Group 3           | 3 winning numbers | 15% of total pot |
| Group 4           | 4 winning numbers | 25% of total pot |
| Group 5 (Jackpot) | 5 winning numbers | 50% of total pot |

### Additional Details:
- In case multiple tickets win within the same prize group, the reward will be equally divided among all winning ticket holders.
- Any remaining funds in the pot after the draw will be carried over to the subsequent draw.


