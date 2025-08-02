using System;
using System.Collections.Generic;
using System.Linq;

public static class Algorithm
{
    public static List<WeightedPost> Posts { get; private set; }

    public static void Init(List<Post> posts) =>
        Posts = posts.Select(post => new WeightedPost { post = post, weight = 3 }).ToList();

    public static List<Post> GetPosts(BossStage stage)
    {
        switch (stage)
        {
            case BossStage.ExGirlfriend:
                {
                    List<Post> posts = new();
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Cat), 2));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Jewelry, posts), 2));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Astrology, posts), 2));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Advertisement, posts), 1));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.AI, posts), 1));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Brainrot, posts), 1));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Crypto, posts), 1));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Education, posts), 1));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Gym, posts), 1));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.HumbleBrag, posts), 1));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Motivational, posts), 1));
                    return Shuffle(posts);
                }
            case BossStage.CryptoBro:
                {
                    List<Post> posts = new();
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Crypto), 3));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Gym, posts), 3));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.HumbleBrag, posts), 3));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Any, posts), 16));
                    return Shuffle(posts);
                }
            case BossStage.CEO:
                {
                    List<Post> posts = new();
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.AI), 4));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Motivational, posts), 4));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Education, posts), 4));
                    posts.AddRange(SamplePosts(GetPostsWithTag(Tags.Any, posts), 28));
                    return Shuffle(posts);
                }
        }

        throw new Exception("Unknown boss stage when attempting to get posts: " + stage);
    }

    public static List<WeightedPost> GetPostsWithTag(Tags tag, List<Post> exclude = null)
    {
        exclude ??= new List<Post>();
        return Posts.Where(post =>
            !exclude.Contains(post.post)
            && (tag == Tags.Any || post.post.postTags.Contains(tag))
        ).ToList();
    }

    public static List<Post> SamplePosts(List<WeightedPost> posts, int amount)
    {
        List<Post> result = new();

        for (int i = 0; i < amount; i++)
        {
            float totalWeight = posts.Sum(post => post.weight);
            totalWeight = UnityEngine.Random.Range(0, totalWeight);
            WeightedPost post = posts.First(post =>
            {
                totalWeight -= post.weight;
                return totalWeight <= 0;
            });
            result.Add(post.post);
            posts.Remove(post);
        }

        return result;
    }

    public static List<Post> Shuffle(List<Post> posts)
    {
        // Fisher-Yates shuffle
        List<Post> result = new(posts);
        for (int i = result.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (result[j], result[i]) = (result[i], result[j]);
        }

        return result;
    }

    public static void Interact(Post post, int weightChange)
    {
        WeightedPost postToChange = Posts.First(p => p.post == post);
        postToChange.weight += weightChange;
    }
}

public struct WeightedPost
{
    public Post post;
    public int weight;
}
