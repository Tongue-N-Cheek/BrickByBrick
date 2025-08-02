using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Algorithm
{
    public static List<WeightedTags> WeightedTags { get; private set; }

    public static void Init(List<Post> posts)
    {
        HashSet<Tags> allTags = new();
        foreach (Post post in posts)
        {
            allTags.UnionWith(post.postTags);
        }
        WeightedTags = new(allTags.Select(tag => new WeightedTags() {tag = tag, weight = 3}));
    }

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

    public static List<Post> GetPostsWithTag(Tags tag, List<Post> exclude = null)
    {
        exclude ??= new List<Post>();
        return GameManager.Instance.AllPosts.Where(post =>
            !exclude.Contains(post)
            && (tag == Tags.Any || post.postTags.Contains(tag))
        ).ToList();
    }

    public static List<Post> SamplePosts(List<Post> posts, int amount)
    {
        List<Post> result = new();

        for (int i = 0; i < amount; i++)
        {
            List<WeightedTags> tags =
                WeightedTags.Where(t => posts.Exists(post => post.postTags.Contains(t.tag))).ToList();

            float totalWeight = tags.Sum(t => t.weight);
            totalWeight = UnityEngine.Random.Range(0, totalWeight);
            WeightedTags tag = tags.First(t =>
            {
                totalWeight -= t.weight;
                return totalWeight <= 0;
            });

            List<Post> filteredPosts = posts.Where(post => post.postTags.Contains(tag.tag)).ToList();
            Post post = filteredPosts[UnityEngine.Random.Range(0, filteredPosts.Count)];

            result.Add(post);
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

    public static void Interact(List<Tags> tags, int weightChange)
    {
        foreach (Tags tag in tags)
        {
            WeightedTags weightedTag = WeightedTags.First(t => t.tag == tag);
            WeightedTags.RemoveAll(t => t.tag == tag);
            weightedTag.weight = Mathf.Max(weightedTag.weight + weightChange, 1);
            WeightedTags.Add(weightedTag);
            Debug.Log(tag + ": " + weightedTag.weight);
        }
    }
}

public struct WeightedTags
{
    public Tags tag;
    public int weight;
}
