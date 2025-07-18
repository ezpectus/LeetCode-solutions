/*
    Problem: Meeting Rooms III (LeetCode #2402)

    Time complexity: O(m log n), where m = number of meetings and n = number of rooms
    Space complexity: O(n)

    Idea:
    - We manage two priority queues:
        1. `usedRooms`: keeps track of currently occupied rooms (sorted by earliest available time)
        2. `unusedRooms`: keeps track of available rooms (always take the smallest index)
    - Process meetings in sorted order by start time.
    - If any rooms are free before a meeting starts → release them into `unusedRooms`.
    - If there's an unused room → assign it and push its end time into `usedRooms`.
    - If all rooms are busy → wait until the earliest one becomes free, and delay the meeting accordingly.
    - Track the number of times each room is used.
    - Return the index of the room with the highest usage count.

    Example:
    meetings = [[0,10],[1,5],[2,7],[3,4]]
    n = 2
    → room 0 takes [0,10], room 1 takes [1,5], [5,10]
    → room 0 used 1x, room 1 used 2x → result: 1
*/

class Solution {
public:
    int mostBooked(int n, vector<vector<int>>& meetings) {
        vector<int> meetingCount(n, 0);
        priority_queue<pair<long long, int>, vector<pair<long long, int>>, greater<pair<long long, int>>> usedRooms;
        priority_queue<int, vector<int>, greater<int>> unusedRooms;
        for (int i = 0; i < n; i++) {
            unusedRooms.push(i);
        }
        sort(meetings.begin(), meetings.end());

        for (auto meeting : meetings) {
            int start = meeting[0], end = meeting[1];

            while (!usedRooms.empty() && usedRooms.top().first <= start) {
                int room = usedRooms.top().second;
                usedRooms.pop();
                unusedRooms.push(room);
            }
            if (!unusedRooms.empty()) {
                int room = unusedRooms.top();
                unusedRooms.pop();
                usedRooms.push({end, room});
                meetingCount[room]++;
            } else {
                auto [roomAvailabilityTime, room] = usedRooms.top();
                usedRooms.pop();
                usedRooms.push({roomAvailabilityTime + end - start, room});
                meetingCount[room]++;
            }
        }

        int maxMeetingCount = 0, maxMeetingCountRoom = 0;
        for (int i = 0; i < n; i++) {
            if (meetingCount[i] > maxMeetingCount) {
                maxMeetingCount = meetingCount[i];
                maxMeetingCountRoom = i;
            }
        }
        return maxMeetingCountRoom;
    }
};
