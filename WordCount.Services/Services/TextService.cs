using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using WordCount.Core;
using WordCount.Core.Models;
using WordCount.Core.Resources;
using WordCount.Core.Services;

namespace WordCount.Services.Services
{
    public class TextService : ITextService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITextCounterService _textCounterService;
        private readonly IMapper _mapper;
        private readonly IDataInit _dataInit;

        public TextService(IUnitOfWork unitOfWork, ITextCounterService textCounterService, IMapper mapper,
            IDataInit dataInit)
        {
            _unitOfWork = unitOfWork;
            _textCounterService = textCounterService;
            _mapper = mapper;
            _dataInit = dataInit;
        }

        private async Task<Text> CreateText(Text newText)
        {
            await _unitOfWork.Texts
                .AddAsync(newText);
            await _unitOfWork.CommitAsync();
            _dataInit.AddData(newText);
            return await GetTextById(newText.TextId);
        }

        public async Task<TextResource> CreateTextTyping(SaveTextResource saveTextResource)
        {
            var text = _mapper.Map<SaveTextResource, Text>(saveTextResource);
            text.TextWordCount = _textCounterService.CountWords(text.TextString);
            var newText = await CreateText(text);
            return _mapper.Map<Text, TextResource>(newText);
        }

        public async Task<TextResource> CreateTextDb(int textId)
        {
            var text = await GetTextById(textId);
            if (text == null)
                return null;

            var newText = new Text
            {
                TextId = GetAllTexts().Result.Count() + 1,
                TextString = text.TextString,
                TextWordCount = _textCounterService.CountWords(text.TextString)
            };

            newText = await CreateText(newText);
            return _mapper.Map<Text, TextResource>(newText);
        }

        public async Task<TextResource> CreateTextFile(string path)
        {
            string[] lines;
            try
            {
                lines = await File.ReadAllLinesAsync(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            var textString = string.Join(" ", lines);

            var newText = new Text
            {
                TextString = textString,
                TextWordCount = _textCounterService.CountWords(textString)
            };
            newText = await CreateText(newText);
            return _mapper.Map<Text, TextResource>(newText);
        }

        public async Task DeleteText(int id)
        {
            var text = await _unitOfWork.Texts.GetByIdAsync(id);
            _unitOfWork.Texts.Remove(text);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<TextResource>> GetAllTexts()
        {
            var texts = await _unitOfWork.Texts.GetAllAsync();
            var textResources = _mapper.Map<IEnumerable<Text>, IEnumerable<TextResource>>(texts);
            return textResources;
        }

        public async Task<Text> GetTextById(int id)
        {
            return await _unitOfWork.Texts.GetByIdAsync(id);
        }

        public async Task UpdateText(int id, SaveTextResource text)
        {
            var textToBeUpdated = await GetTextById(id);
            textToBeUpdated.TextString = text.TextString;
            textToBeUpdated.TextWordCount = _textCounterService.CountWords(text.TextString);

            await _unitOfWork.CommitAsync();
        }
    }
}